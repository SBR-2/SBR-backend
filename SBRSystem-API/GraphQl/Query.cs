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
        

        return lst2.AsQueryable();
    }  
}