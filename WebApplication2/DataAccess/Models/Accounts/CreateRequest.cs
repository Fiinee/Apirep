using System.ComponentModel.DataAnnotations;
using WebApplication2.Entities;

namespace WebApplication2.DataAccess.Models.Accounts
{
    public class CreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}