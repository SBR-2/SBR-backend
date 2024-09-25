using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class BpmSubcategorium
{
    public int BpmSubcategoriaId { get; set; }

    public int? BpmCategoriaId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual BpmCategorium? BpmCategoria { get; set; }

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
