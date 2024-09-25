using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class BpmCategorium
{
    public int BpmCategoriaId { get; set; }

    public int? GrupoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<BpmSubcategorium> BpmSubcategoria { get; set; } = new List<BpmSubcategorium>();

    public virtual Grupo? Grupo { get; set; }
}
