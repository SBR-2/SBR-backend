using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Comentario
{
    public int ComentarioId { get; set; }

    public int FichaId { get; set; }

    public int Numero { get; set; }

    public string Detalle { get; set; } = null!;

    public DateTime FechaCumplimiento { get; set; }

    public virtual Ficha Ficha { get; set; } = null!;
}
