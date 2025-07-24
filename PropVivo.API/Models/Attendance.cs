using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public double? TotalWorkHours { get; set; }

    public double? TotalBreakTime { get; set; }

    public bool? Completed { get; set; }

    public virtual UserMaster User { get; set; } = null!;
}
