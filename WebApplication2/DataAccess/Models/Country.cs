using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class Country
{
    public string Name { get; set; } = null!;

    public int Population { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
