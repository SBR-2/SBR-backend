using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Respuestum
{
    public int RespuestaId { get; set; }

    public int? PreguntaId { get; set; }

    public int? FichaId { get; set; }

    public int? ValorId { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual Ficha? Ficha { get; set; }

    public virtual Preguntum? Pregunta { get; set; }

    public virtual Valor? Valor { get; set; }
}
