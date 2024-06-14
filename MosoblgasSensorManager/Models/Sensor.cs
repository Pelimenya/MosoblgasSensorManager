using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Sensor
{
    public int SensorId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public DateOnly InstallationDate { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual ICollection<Qrcode> Qrcodes { get; set; } = new List<Qrcode>();

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
