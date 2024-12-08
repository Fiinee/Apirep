using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class CommentAgency
{
    public int CommentId { get; set; }

    public int Agency { get; set; }

    public string CommentText { get; set; } = null!;

    public virtual Agency AgencyNavigation { get; set; } = null!;
}
