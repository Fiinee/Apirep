using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Entities;

namespace WebApplication2.DataAccess.Models
{

    public partial class Account
    {
        
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        [StringLength(250)]
        public string Password { get; set; } = null!;
        public Role Role { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UsersCart> UsersCarts { get; set; } = new List<UsersCart>();
        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }

    }
}