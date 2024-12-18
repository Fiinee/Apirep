using Microsoft.Extensions.Options;
using WebApplication2.DataAccess.Models;
using WebApplication2.Helpers;
using WebApplication2.Interfaces;

namespace WebApplication2.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, PractikaContext wrapper, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null) {
                context.Items["Account"] = accountId.Value;
            }
            await _next(context);
        }
        
    }
}
