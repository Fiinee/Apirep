using WebApplication2.DataAccess.Models;
using WebApplication2.Entities;

namespace WebApplication2.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Account account);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
