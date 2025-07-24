using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class QuerieMaster
{
    public int QueryId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    public string? Subject { get; set; }

    public string? Description { get; set; }

    public string? AttachmentPath { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<QueryResponse> QueryResponses { get; set; } = new List<QueryResponse>();

    public virtual TaskMaster? Task { get; set; }
}
