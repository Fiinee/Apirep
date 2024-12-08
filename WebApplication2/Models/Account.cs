using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UsersCart> UsersCarts { get; set; } = new List<UsersCart>();
}
