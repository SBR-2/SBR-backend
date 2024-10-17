using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using SBRSystem_Entities.Contracts;
using GraphQLException = HotChocolate.GraphQLException;
using Path = System.IO.Path;


namespace SBRSystem_API.GraphQl.Mutation
{
    [MutationType]
    public class DocumentosMutation
    {
        public async Task<Documento> AddDocumento(AddDocumentoInput documentoInput, [Service] MySBRDbContext context
            , [Service] IBucketRepository _bucketRepository)
        {
            if (documentoInput.TipoDocumentoId > 19 || documentoInput.TipoDocumentoId < 1)
            {
                throw new GraphQLException("Tipo de documento invalido. Opciones validas: 1 - 19");
            }

            ICollection<ComentarioDocumento> ComentarioDocumentos = documentoInput.ComentarioDocumentos.Select(
                comentario => new ComentarioDocumento
                {
                    UsuarioId = comentario,
                }).ToList();

            //subir archivo 


            string folderPath = $"{BucketConfig.SolicitudesFolder}/Documentos-{documentoInput.SolicitudId}";
            var fileExt = Path.GetExtension(documentoInput.archivo.Name);
            Guid nuevoID = Guid.NewGuid();

            string objName = $"{folderPath}/archivo-{nuevoID}{fileExt}";

            var s3obj = await S3Object.CrearObjeto(objName, documentoInput.archivo);

            S3ResponseDto result = await _bucketRepository.UploadFileAsync(s3obj);


            //end subir archivo

            Documento newDocumento = new Documento
            {
                Ruta = result.Message,
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