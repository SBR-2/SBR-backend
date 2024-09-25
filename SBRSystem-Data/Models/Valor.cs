using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Valor
{
    public int ValorId { get; set; }

    public string? NomenclaturaValor { get; set; }

    public int? Puntos { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
