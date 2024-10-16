using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using GraphQLException = HotChocolate.GraphQLException;


namespace SBRSystem_API.GraphQl.Mutation
{
    [MutationType]
    public class SolicitudMutation
    {
        public async Task<Solicitud> AddSolicitud(AddSolicitudInput solicitudInput, [Service] MySBRDbContext context)
        {
            if (solicitudInput.Estado != "en proceso" && solicitudInput.Estado != "rechazada" && solicitudInput.Estado != "aceptada")
            {
                throw new GraphQLException("Estado de solicitud invalido. Opciones validas: 'en proceso', 'rechazada', 'aceptada'");
            }

            Solicitud newSolicitud = new Solicitud
            {
                AcondicionadorDistinto = solicitudInput.AcondicionadorDistinto,
                EsExportado = solicitudInput.EsExportado,
                Estado = solicitudInput.Estado,
                FechaCreacion = DateTime.Now,
                Observaciones = solicitudInput.Observaciones,
                TitularFabricante = solicitudInput.TitularFabricante,
                TitularRepresentacion = solicitudInput.TitularRepresentacion,
                ProductoId = solicitudInput.ProductoId,
                RiesgoTotal = 0
            };

            try
            {
                context.Solicituds.Add(newSolicitud);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new GraphQLException("Ocurrió un error al agregar la solicitud.");
            }

            return newSolicitud;
        }

        public async Task<Solicitud> UpdateSolicitud(UpdateSolicitudInput solicitudInput, [Service] MySBRDbContext context)
        {
            if (solicitudInput.Estado != "en proceso" && solicitudInput.Estado != "rechazada" && solicitudInput.Estado != "aceptada")
            {
                throw new GraphQLException("Estado de solicitud invalido. Opciones validas: 'en proceso', 'rechazada', 'aceptada'");
            }

            Solicitud solicitud = await context.Solicituds.FindAsync(solicitudInput.SolicitudId);

            if (solicitud == null)
            {
                throw new GraphQLException("No se encontró la solicitud con el id proporcionado.");
            }

            solicitud.AcondicionadorDistinto = solicitudInput.AcondicionadorDistinto;
            solicitud.EsExportado = solicitudInput.EsExportado;
            solicitud.Estado = solicitudInput.Estado;
            solicitud.Observaciones = solicitudInput.Observaciones;
            solicitud.TitularFabricante = solicitudInput.TitularFabricante;
            solicitud.TitularRepresentacion = solicitudInput.TitularRepresentacion;
            solicitud.ProductoId = solicitudInput.ProductoId;
            solicitud.RiesgoTotal = 0;

            try
            {
                context.Solicituds.Update(solicitud);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new GraphQLException("Ocurrió un error al actualizar la solicitud.");
            }

            return solicitud;
        }

        public async Task<Solicitud> RechazarSolicitud(RechazarSolicitudInput solicitudInput, [Service] MySBRDbContext context)
        {
            Solicitud solicitud = await context.Solicituds.FindAsync(solicitudInput.SolicitudId);

            if (solicitud == null)
            {
                throw new GraphQLException("No se encontró la solicitud con el id proporcionado.");
            }

            solicitud.Estado = "rechazada";
            solicitud.FechaRechazo = DateTime.Now;

            try
            {
                context.Solicituds.Update(solicitud);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new GraphQLException("Ocurrió un error al rechazar la solicitud.");
            }

            return solicitud;
        }
    }
}
