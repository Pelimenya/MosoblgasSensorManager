using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Reading
{
    public int ReadingId { get; set; }

    public int SensorId { get; set; }

    public DateOnly ReadingDate { get; set; }

    public decimal Value { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;
}
