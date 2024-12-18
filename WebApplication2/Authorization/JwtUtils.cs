using WebApplication2.Helpers;
using WebApplication2.Interfaces;
using Microsoft.Extensions.Options;
using WebApplication2.Authorization;
using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace WebApplication2.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly AppSettings _appSettings;

        public JwtUtils(IRepositoryWrapper wrapper, AppSettings appSettings)
        {
            _wrapper = wrapper;
            _appSettings = appSettings;
        }

        public string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()), new Claim(ClaimTypes.Role, account.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
            var tokrnIsUnique =(await _wrapper.Accounts.Where(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token))).CountAsync == 0;
            if (!tokrnIsUnique) return await GenerateRefreshToken(ipAddress);
            return refreshToken;
        }

        public int? ValidateJwtToken(string token)
        {
            if(token == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken valiateToken);
                var jwtToken = (JwtSecurityToken)valiateToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                return accountId;
            }
            catch
            {
                return null;
            }
        }
    }
}
