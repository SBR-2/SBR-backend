using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

/// <summary>
/// Periodicidad en base al riesgo total en que frecuencia se tiene que evaluar nuevamente 
/// </summary>
public partial class MatizRiesgo
{
    public int MatizRiesgoId { get; set; }

    public int Estado { get; set; }

    public virtual ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();
}
