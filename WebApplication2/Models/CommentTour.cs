using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class CommentTour
{
    public int CommentId { get; set; }

    public int TourPlan { get; set; }

    public string CommentText { get; set; } = null!;

    public virtual TourPlan TourPlanNavigation { get; set; } = null!;
}
