using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DataAccess.Models.Accounts
{
    namespace BusinessLogic.Models.Accounts
    {
        public class ForgotPasswordRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
