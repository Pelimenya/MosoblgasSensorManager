using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Technician
{
    public int TechnicianId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? Qualification { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual User TechnicianNavigation { get; set; } = null!;
}
