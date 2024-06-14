using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int SensorId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string MaintenanceType { get; set; } = null!;

    public int TechnicianId { get; set; }

    public string? Notes { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;

    public virtual Technician Technician { get; set; } = null!;
}
