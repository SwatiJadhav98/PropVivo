using System;
using System.Collections.Generic;

namespace PropVivo.API.Models;

public partial class QueryResponse
{
    public int ResponseId { get; set; }

    public int? QueryId { get; set; }

    public int? ResponderId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual QuerieMaster? Query { get; set; }

    public virtual UserMaster? Responder { get; set; }
}
