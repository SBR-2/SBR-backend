using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using System.Linq;
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

        public async Task<Solicitud> AsignarOpcionesASolicitud(AsignarOpcionesASolicitudInput solicitudInput, [Service] MySBRDbContext context)
        {
            Solicitud solicitud = await context.Solicituds.FindAsync(solicitudInput.SolicitudId);

            if (solicitud == null)
            {
                throw new GraphQLException("No se encontró la solicitud con el id proporcionado.");
            }

            // Clear existing opciones
            solicitud.Opcions.Clear();

            List<int?> factorsList = new List<int?>();

            // Fetch opciones from the database based on their IDs
            foreach (var opcionInputId in solicitudInput.OpcionIds)
            {
                Opcion opcion = await context.Opcions.FindAsync(opcionInputId);

                if (opcion == null)
                {
                    throw new GraphQLException($"No se encontró la opción con el id {opcionInputId}. Opciones disponibles: 26. No tiene implementado el sistema HACCP, 27. Tiene implementado el HACCP en el 25% de las líneas de producción, 28. Tiene implementado el HACCP en el 75% de las líneas de producción, 29. Tiene implementado el HACCP en todas las líneas de producción, 30. ≤ 60%, 31. > 60% - 70%, 32. > 70% - 80%, 33. > 80%, 34. Los productos que elaboran los sirven a nivel nacional, 35. Los productos que elaboran los sirven a nivel regional, 36. Los productos que elaboran los sirven a nivel local, 37. No es suplidor del INABIE, 38. Grande (> 2.000.000 por mes), 39. Mediano (800.000 - 2.000.000 por mes), 40. Pequeño (200.000 - 800.000 por mes), 41. Micro (< 200.000 por mes), 42. Tiene más de 2 rechazos en los últimos 5 años, 43. Tiene 2 rechazos en los últimos 5 años, 44. Tiene 1 rechazo en los últimos 5 años, 45. No tiene ningún rechazo en los últimos 5 años, 46. No cuenta con plan de muestreo microbiológico, 47. Plan de muestreo en materias primas, 48. Plan de muestreo en áreas de proceso y producto terminado, 49. Plan de muestreo en materias primas y áreas de proceso y producto terminado");
                }

                if (factorsList.Contains(opcion.FactorId))
                {
                    throw new GraphQLException($"La opción con el id {opcionInputId} no se puede añadir debido a una colision de factores.");
                }

                factorsList.Add(opcion.FactorId);
                solicitud.Opcions.Add(opcion);
            }

            solicitud.RiesgoTotal = await CalcularRiesgoTotal(solicitud, context);

            try
            {
                context.Solicituds.Update(solicitud);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new GraphQLException("Ocurrió un error al asignar las opciones a la solicitud.");
            }

            return solicitud;
        }

        async Task<int> CalcularRiesgoTotal(Solicitud solicitud, MySBRDbContext context)
        {
            if (solicitud.Producto is not null || solicitud.Opcions is null)
            {
                return 0;
            }

            int riesgoTotal = 0;

            Console.WriteLine(solicitud.Opcions.Count);
            // Console.WriteLine(solicitud.Producto.RiesgoSubcategoria.Riesgo.Riesgo1);

            foreach (var opcion in solicitud.Opcions)
            {
                Console.WriteLine(opcion.Valor);
                riesgoTotal += Convert.ToInt32(opcion.Valor) * (Convert.ToInt32(context.Factors.FindAsync(opcion.FactorId).Result.Peso / 100));
            }

            //switch (solicitud.Producto.RiesgoSubcategoria.Riesgo.Riesgo1)
            //{
            //    case "BAJO":
            //        riesgoTotal *= 1;
            //        break;
            //    case "MEDIO":
            //        riesgoTotal *= 2;
            //        break;
            //    case "ALTO":
            //        riesgoTotal *= 3;
            //        break;
            //    default:
            //        break;
            //}

            return riesgoTotal;
        }
    }
}
