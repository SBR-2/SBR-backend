using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Factor
{
    public int FactorId { get; set; }

    public string? FactorNombre { get; set; }

    public string? Nombre { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}
