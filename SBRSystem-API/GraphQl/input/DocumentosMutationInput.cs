using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.input
{
    public class AddDocumentoInput
    {
        public string Ruta { get; set; }

        public ICollection<int> ComentarioDocumentos { get; set; } = new List<int>();

        public int SolicitudId { get; set; }

        public int TipoDocumentoId { get; set; }
    }
}
