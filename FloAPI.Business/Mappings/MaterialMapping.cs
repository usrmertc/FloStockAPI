using FloAPI.Business.Models;

namespace FloAPI.Business.Mappings;

public static class MaterialMapping
{
    public static Material EntityToModel(FloAPI.DataAccess.Entities.Material material)
    {
        return new Material(
            id: material.Id,
            name: material.Name,
            count: material.Count,
            thresholdLimit: material.ThresholdLimit

        );
    }
    
    public static FloAPI.DataAccess.Entities.Material ModelToEntity(Material material)
    {
        return new FloAPI.DataAccess.Entities.Material
        {
            Name = material.Name,
            Count = material.Count,
            ThresholdLimit = material.ThresholdLimit
        };
    }
    
    public static FloAPI.DataAccess.Entities.Material ModelToEntityWithId(Material material)
    {
        return new FloAPI.DataAccess.Entities.Material
        {
            Id = material.Id,
            Name = material.Name,
            Count = material.Count,
            ThresholdLimit = material.ThresholdLimit
        };
    }
}