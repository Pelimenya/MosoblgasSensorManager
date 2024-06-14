using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public DateOnly GeneratedDate { get; set; }

    public string ReportType { get; set; } = null!;

    public string Content { get; set; } = null!;
}
