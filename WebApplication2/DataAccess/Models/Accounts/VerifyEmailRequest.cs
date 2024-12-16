using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DataAccess.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
   
}
