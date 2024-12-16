using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DataAccess.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
  
}
