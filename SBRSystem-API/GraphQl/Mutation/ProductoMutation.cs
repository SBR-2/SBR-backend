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
        if (await context.Productos.AnyAsync(p => p.Nombre == input.Nombre))
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

        context.Productos.Add(newProducto);

        try
        {
            await context.SaveChangesAsync();

            foreach (var productoEntidad in input.ProductoEntidades)
            {
                productoEntidad.ProductoId = newProducto.ProductoId;
                context.ProductoEntidads.Add(productoEntidad);
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error(
                $"Ocurrio un error tratando de introducir datos en la base de datos. \n Error Mesagge: {ex.Message}");
            throw new GraphQLException("Ocurrio un error al tratar de agregar un producto", ex);
        }

        return newProducto;
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