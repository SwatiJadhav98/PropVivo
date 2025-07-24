using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class UserMaster
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<QueryResponse> QueryResponses { get; set; } = new List<QueryResponse>();

    public virtual ICollection<TaskMaster> TaskMasterAssignedToNavigations { get; set; } = new List<TaskMaster>();

    public virtual ICollection<TaskMaster> TaskMasterCreatedByNavigations { get; set; } = new List<TaskMaster>();

    public virtual ICollection<TimeLogMaster> TimeLogMasters { get; set; } = new List<TimeLogMaster>();
}
