using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class TaskMaster
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public double? EstimatedHours { get; set; }

    public int? AssignedTo { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual UserMaster? AssignedToNavigation { get; set; }

    public virtual UserMaster? CreatedByNavigation { get; set; }

    public virtual ICollection<QuerieMaster> QuerieMasters { get; set; } = new List<QuerieMaster>();

    public virtual ICollection<TimeLogMaster> TimeLogMasters { get; set; } = new List<TimeLogMaster>();
}
