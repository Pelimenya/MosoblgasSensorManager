using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Address { get; set; } = null!;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
}
