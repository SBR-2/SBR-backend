using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Provincium
{
    public int ProvinciaId { get; set; }

    public string Provincia { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
