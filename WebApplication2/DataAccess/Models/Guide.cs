using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class Guide
{
    public int EmployeeCode { get; set; }

    public string Name { get; set; } = null!;

    public int Agency { get; set; }

    public double Rating { get; set; }

    public virtual Agency AgencyNavigation { get; set; } = null!;

    public virtual ICollection<Tour2> Tour2s { get; set; } = new List<Tour2>();
}
