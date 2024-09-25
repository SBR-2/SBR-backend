using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Grupo
{
    public int GrupoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<BpmCategorium> BpmCategoria { get; set; } = new List<BpmCategorium>();
}
