using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Opcion
{
    public int OpcionId { get; set; }

    public int? FactorId { get; set; }

    public string? Valor { get; set; }

    public string? Detalle { get; set; }

    public bool? Estado { get; set; }

    public virtual Factor? Factor { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
