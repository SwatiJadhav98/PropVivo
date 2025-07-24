using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class TimeLogMaster
{
    public int TimeLogId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? DurationMinutes { get; set; }

    public bool? IsBreak { get; set; }

    public string? BreakReason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TaskMaster? Task { get; set; }

    public virtual UserMaster? User { get; set; }
}
