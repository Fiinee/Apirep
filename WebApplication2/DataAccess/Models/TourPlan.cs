using System;
using System.Collections.Generic;

namespace WebApplication2.DataAccess.Models;

public partial class TourPlan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CityId { get; set; }

    public double Rating { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<CommentTour> CommentTours { get; set; } = new List<CommentTour>();

    public virtual ICollection<Tour2> Tour2s { get; set; } = new List<Tour2>();
}
