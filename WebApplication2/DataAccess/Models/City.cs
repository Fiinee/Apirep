using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual Country CountryNavigation { get; set; } = null!;

    public virtual ICollection<TourPlan> TourPlans { get; set; } = new List<TourPlan>();
}
