using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class MercadoObjetivo
{
    public int MercadoObjetivoId { get; set; }

    public string MercadoNombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();
}
