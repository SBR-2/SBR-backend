using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Municipio
{
    public int MunicipioId { get; set; }

    public int? ProvinciaId { get; set; }

    public string Municipio1 { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();

    public virtual Provincium? Provincia { get; set; }
}
