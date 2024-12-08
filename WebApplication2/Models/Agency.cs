using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Agency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumEmployeer { get; set; }

    public double Rating { get; set; }

    public virtual ICollection<CommentAgency> CommentAgencies { get; set; } = new List<CommentAgency>();

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();
}
