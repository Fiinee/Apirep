using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class UsersCart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TourCode { get; set; }

    public virtual Tour2 TourCodeNavigation { get; set; } = null!;

    public virtual Account User { get; set; } = null!;
}
