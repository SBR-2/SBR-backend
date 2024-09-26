using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Establecimiento
{
    public int EstablecimientoId { get; set; }

    public string? Nombre { get; set; }

    public string? Numero { get; set; }

    public string? Municipio { get; set; }

    public string? Provincia { get; set; }

    public string? Telefono { get; set; }

    public DateTime? InicioOperaciones { get; set; }

    public DateTime? VencimientoSanitario { get; set; }

    public string? Rnc { get; set; }

    public int? NumProductosElaborados { get; set; }

    public int? ProduccionAnual { get; set; }

    public string? Comercializacion { get; set; }

    public string? MercadoObjetivo { get; set; }

    public int? NumEmpleados { get; set; }

    public DateTime? UltimaInspeccion { get; set; }

    public long? CalUltimaInspeccion { get; set; }

    public string? NombreDps { get; set; }

    public string? NombreDigemaps { get; set; }

    public bool? NoSanitario { get; set; }

    public virtual ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();
}
