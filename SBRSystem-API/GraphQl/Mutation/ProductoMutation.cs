using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using Serilog;

namespace SBRSystem_API.GraphQl.Mutation;

[MutationType]
public class ProductoMutation
{
    public async Task<Producto> AddProductoAsync(AddProductoInput input,
        [Service] MySBRDbContext context)
    {
        if (await context.Productos.AnyAsync(p => p.Nombre == input.Nombre))
        {
            throw new GraphQLException("El riesgo ya existe");
        }

        var newProducto = new Producto
        {
            Nombre = input.Nombre,
            Marca = input.Marca,
            Origen = input.Origen,
            Estado = input.Estado,
            Presentaciones = input.Presentaciones,
            EstadoFisicoId= input.EstadoFisicoId,
            EnvasePrimario = input.EnvasePrimario,
            MaterialEmpaque = input.MaterialEmpaque,
            Nacional = input.Nacional,
            UnIngrediente = input.UnIngrediente
        };
        
        context.Productos.Add(newProducto);

        try
        {

        }
        catch (Exception ex)
        {
            Log.Error($"Ocurrio un error tratando de introducir datos en la base de datos. \n Error Mesagge: {ex.Message}");
            throw new GraphQLException("Ocurrio un error al tratar de agregar un producto", ex);
        }
        
        return newProducto;
    }

    public async Task<Producto> DeleteProducto(DeleteProductoInput input, [Service] MySBRDbContext context)
    {
        var producto = await context.Productos.FindAsync(input.ProductoId);

        if (producto == null)
        {
            Log.Error("Producto no encontrado");
            throw new GraphQLException("El producto no existe");
        }

        if (producto.Estado == "false")
        {
            Log.Error("El producto ya fue eliminado");
        }
        
        producto.Estado = "false";

        try
        {

        }
        catch (DbUpdateException ex)
        {
            Log.Error($"Error al eliminar el producto{ex}");
            throw new GraphQLException($"Error al tratar de eliminar el producto{ex}");
        }

        return producto;
    }
}