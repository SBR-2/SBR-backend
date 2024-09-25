using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Preguntum
{
    public int PreguntaId { get; set; }

    public int? BpmSubcategoriaId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual BpmSubcategorium? BpmSubcategoria { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
