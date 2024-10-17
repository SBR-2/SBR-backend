using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using Serilog;

namespace SBRSystem_API.GraphQl.Mutation;

[MutationType]
public class ProductoMutation
{
    public async Task<Producto> AddProductoAsync(AddProductoInput input, [Service] MySBRDbContext context)
    {
        if (await context.Productos.AnyAsync(p => p.Nombre == input.Nombre && p.UsuarioId == input.UsuarioId))
        {
            throw new GraphQLException("El producto ya existe");
        }

        var newProducto = new Producto
        {
            Nombre = input.Nombre,
            Marca = input.Marca,
            Origen = input.Origen,
            Estado = input.Estado,
            Presentaciones = input.Presentaciones,
            RiesgoSubcategoriaId = input.RiesgoSubcategoriaId,
            UsuarioId = input.UsuarioId,
            EstadoFisicoId = input.EstadoFisicoId,
            EnvasePrimario = input.EnvasePrimario,
            MaterialEmpaque = input.MaterialEmpaque,
            Nacional = input.Nacional,
            UnIngrediente = input.UnIngrediente,
        };
        Producto newproduct;

        try
        {
            context.Productos.Add(newProducto);
            await context.SaveChangesAsync();
            newproduct =
                context.Productos.First(x => x.Nombre == newProducto.Nombre && x.UsuarioId == input.UsuarioId);

            foreach (var productoEntidad in input.ProductoEntidades)
            {
                productoEntidad.ProductoId = newproduct.ProductoId;

                ProductoEntidad puente = new ProductoEntidad
                {
                    ProductoId = productoEntidad.ProductoId,
                    EntidadId = productoEntidad.EntidadId,
                    RelacionId = productoEntidad.RelacionId,
                };
                context.ProductoEntidads.Add(puente);
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error(
                $"Ocurrio un error tratando de introducir datos en la base de datos. \n Error Mesagge: {ex.Message}");
            throw new GraphQLException("Ocurrio un error al tratar de agregar un producto", ex);
        }

        return newproduct;
    }

    public async Task<bool> DeleteProductoAsync(int productoId, [Service] MySBRDbContext context)
    {
        var producto = await context.Productos
            .Include(p => p.ProductoEntidads)
            .FirstOrDefaultAsync(p => p.ProductoId == productoId);

        if (producto == null)
        {
            throw new GraphQLException("Producto not found");
        }

        context.Productos.Remove(producto);
        await context.SaveChangesAsync();

        return true;
    }
}