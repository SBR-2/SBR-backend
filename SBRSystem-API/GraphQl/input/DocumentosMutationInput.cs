using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.input
{
    public class AddDocumentoInput
    {
        public IFile archivo { get; set; }

        public ICollection<int> ComentarioDocumentos { get; set; } = new List<int>();

        public int SolicitudId { get; set; } = 0;

        public int TipoDocumentoId { get; set; } = 0;
    }
}