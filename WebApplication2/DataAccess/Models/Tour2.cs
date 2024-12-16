using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class Tour2
{
    public int TourCode { get; set; }

    public int TourPlan { get; set; }

    public int GuideCode { get; set; }

    public DateTime DateTime { get; set; }

    public virtual Guide GuideCodeNavigation { get; set; } = null!;

    public virtual TourPlan TourPlanNavigation { get; set; } = null!;

    public virtual ICollection<UsersCart> UsersCarts { get; set; } = new List<UsersCart>();
}
