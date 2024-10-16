using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using GraphQLException = HotChocolate.GraphQLException;


namespace SBRSystem_API.GraphQl.Mutation
{
    [MutationType]
    public class DocumentosMutation
    {
        public async Task<Documento> AddDocumento(AddDocumentoInput documentoInput, [Service] MySBRDbContext context)
        {
            if (documentoInput.TipoDocumentoId > 19 || documentoInput.TipoDocumentoId < 1)
            {
                throw new GraphQLException("Tipo de documento invalido. Opciones validas: 1 - 19");
            }

            ICollection<ComentarioDocumento> ComentarioDocumentos = documentoInput.ComentarioDocumentos.Select(comentario => new ComentarioDocumento
            {
                UsuarioId = comentario,
            }).ToList();

            Documento newDocumento = new Documento
            {
                Ruta = documentoInput.Ruta,
                ComentarioDocumentos = ComentarioDocumentos,
                SolicitudId = documentoInput.SolicitudId,
                TipoDocumentoId = documentoInput.TipoDocumentoId
            };

            try
            {
                context.Documentos.Add(newDocumento);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new GraphQLException("Ocurrió un error al agregar el documento.");
            }

            return newDocumento;
        }
    }
}
