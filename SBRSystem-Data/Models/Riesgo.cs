using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Riesgo
{
    public int RiesgoId { get; set; }

    public string? Riesgo1 { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<RiesgoSubcategorium> RiesgoSubcategoria { get; set; } = new List<RiesgoSubcategorium>();
}
