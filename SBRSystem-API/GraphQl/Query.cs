using SBRSystem_Data.Context;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl;

public class Query
{
    public IQueryable<RiesgoDto> GetRiesgos([Service] MySBRDbContext context)
    {
        List<Riesgo> lst = context.Riesgos.ToList();

        List<RiesgoDto> lst2 = new List<RiesgoDto>();

        lst2.Add(new RiesgoDto
        {
            Estado = true,
            Riesgo1 = "",
            RiesgoId = 1
        });
        

        return lst2.AsQueryable();
    }
    public static IQueryable<Riesgo> GetServicios(MySBRDbContext context) => context.Riesgos;
}