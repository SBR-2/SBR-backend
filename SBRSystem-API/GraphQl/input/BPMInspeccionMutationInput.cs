using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.input;

public class AddGrupoInput
{
    [GraphQLNonNullType] public string Nombre { get; set; }

    [GraphQLNonNullType] public string Descripcion { get; set; }

    [GraphQLNonNullType] public bool Estado { get; set; }
}

public class AddBpmCategoria : AddInputBPM
{
    public int GrupoId { get; set; }
}

public class AddInputBPM
{
    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }
}

public class AddSubBpmCategoria : AddInputBPM
{
    public int BpmCategoriaId { get; set; }
}

public class AddPregunta : AddInputBPM
{
    public int BpmSubcategoriaId { get; set; }
}

public class AddRespuestaInput
{
    public int PreguntaId { get; set; }

    public int FichaId { get; set; }

    public int ValorId { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }
}

public class AddFichaInput
{
    public int? SolicitudId { get; set; }
    public int? EstablecimientoId { get; set; }

    public bool? Estado { get; set; }
    public string? NombreDps { get; set; }
    public string? NombreDigemaps { get; set; }
}

public class UpdateFichaInput
{
    public int FichaId { get; set; }
    public DateTime? FechaRevision { get; set; }
    public DateTime? FechaAprobacion { get; set; }
    public float? Calificacion { get; set; }
    public int? InspectorId { get; set; }
    public int? RevisorId { get; set; }
    public int? AprobadorId { get; set; }
    public int? MatizRiesgo { get; set; }
    public int? EvaluadorId { get; set; }
}

public class AddValorInput
{
    public string NomenclaturaValor { get; set; }

    public int Puntos { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }
}

public class AddEstablecimientoInput
{
    public string Nombre { get; set; }

    public string Numero { get; set; }

    public int MunicipioId { get; set; }

    public string Telefono { get; set; }

    public DateTime InicioOperaciones { get; set; }

    public DateTime VencimientoSanitario { get; set; }

    public string Rnc { get; set; }

    public int NumProductosElaborados { get; set; }

    public int ProduccionAnual { get; set; }

    public string Comercializacion { get; set; }

    public int MercadoObjetivo_id { get; set; }
    

    public int NumEmpleados { get; set; }

    public DateTime UltimaInspeccion { get; set; }

    public long CalUltimaInspeccion { get; set; }

    public string NombreDps { get; set; }

    public string NombreDigemaps { get; set; }

    public string NoSanitario { get; set; }
}