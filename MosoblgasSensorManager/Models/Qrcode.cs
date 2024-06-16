using System;
using System.Collections.Generic;

namespace MosoblgasSensorManager.Models;

public partial class Qrcode
{
    public int QrCodeId { get; set; }

    public string QrCodeData { get; set; } = null!;

    public virtual Sensor QrCode { get; set; } = null!;
}
