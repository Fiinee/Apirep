using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;
using WebApplication2.DataAccess.Models.Accounts;
using WebApplication2.DataAccess.Models.Accounts.BusinessLogic.Models.Accounts;
using WebApplication2.Entities;
using WebApplication2.Helpers;
using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class AccountService : IAccountService
    {
        private readonly PractikaContext _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(
            PractikaContext repositoryWrapper,
            IJwtUtils jwtUtils, 
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        private void removeOldRefreshTokens(Account account)
        {
            account.RefreshTokens.RemoveAll(x => !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repositoryWrapper.Accounts.Include(x => x.ResetToken).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
                throw new AppException("Email or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add( refreshToken);

            removeOldRefreshTokens(account);

            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(model);
            response.JwtToken = jwtToken;
            response.RefreshToken =  refreshToken.Token;
            // return response;
            return response;

        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            var existingUserCount = await _repositoryWrapper.Accounts.Where(x => x.Email == model.Email).CountAsync();

            if (existingUserCount > 0)
            {
                throw new AppException($"Email '{model.Email}' is already registered");
            }
           

            var account = _mapper.Map<Account>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();
        }

        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = await _repositoryWrapper.Accounts.AnyAsync(x => x.ResetToken == token);
            if (!tokenIsUnique)
            {
                return await generateResetToken();
            }
            return token;

        }


        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _repositoryWrapper.Accounts.Where(x => x.Email == model.Email).FirstOrDefault();
            if (account == null) return;
            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

        }

        private async Task<Account> getAccount(int id)
        {

            var account = (_repositoryWrapper.Accounts.Where(x => x.Id == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;


        }
        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (await _repositoryWrapper.Accounts.Where(x => x.Email == model.Email).CountAsync() > 0)
                throw new AppException($"Email '{model.Email} ' is already registered");

            if (!string.IsNullOrEmpty(model.Password))
                account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);

        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            List<Account> accounts = await _repositoryWrapper.Accounts.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(accounts);

        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);

        }
        public Task Register(RegisterRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }
        
        private async Task<Account> getAccountByRefreshToken(string token)
        {
            var account = (_repositoryWrapper.Accounts.Where(x => x.RefreshTokens.Any(
                t=>t.Token == token))).FirstOrDefault();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token");
            return newRefreshToken;
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddres, string reason = null, string replaceByToken= null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevolkedByIp = ipAddres;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replaceByToken;
        }

        private void revokeDescendantRefreshToken(RefreshToken refreshToken, Account account, string ipAddres, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddres, reason);
                else 
                    revokeDescendantRefreshToken(childToken, account, ipAddres, reason); 
               
            }   
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if(refreshToken.IsRevoked)
            {
                revokeDescendantRefreshToken(refreshToken, account, ipAddress, $"Attempted reuse of revolked ancestor token: {token}");
                _repositoryWrapper.Accounts.Update(account);
                await _repositoryWrapper.SaveChangesAsync();
            }
            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");
            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);
            removeOldRefreshTokens(account);
            _repositoryWrapper.Accounts.Update(account);
            await _repositoryWrapper.SaveChangesAsync();
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        } 

        private async Task<string> generateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = await _repositoryWrapper.Accounts.Where(x => x.VerificationToken == token).CountAsync();
            if (tokenIsUnique > 0)
                return await generateVerificationToken();
            return token;
        }

        public async Task Register(RegisterRequest model,  string username, string origin)
        {
            var count = await _repositoryWrapper.Accounts.Where(x => x.VerificationToken == token).CountAsync();

            if (count > 0) return;
            var account = _mapper.Map<Account>(model);
            var isFirstAccount = (await _repositoryWrapper.Accounts.CountAsync() == 0);
        }

        public Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }
    }
}
