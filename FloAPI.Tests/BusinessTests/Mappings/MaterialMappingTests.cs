using FloAPI.Business.Mappings;
using FloAPI.Business.Models;

namespace FloAPI.Tests.BusinessTests.Mappings;

public class MaterialMappingTests
{
    [Fact]
    private void ModelShouldConvertedToEntityWithId()
    {
        Material material = new Material(
            id: 1,
            name: "TestMaterial",
            count: 10,
            thresholdLimit: 5
        );
        
        var result = MaterialMapping.ModelToEntityWithId(material);

        Assert.NotNull(result);
        Assert.IsType<DataAccess.Entities.Material>(result);
        Assert.Equal(material.Id, result.Id);
        Assert.Equal(material.Name, result.Name);
        Assert.Equal(material.Count, result.Count);
        Assert.Equal(material.ThresholdLimit, result.ThresholdLimit);
    }
    
    [Fact]
    private void ModelShouldConvertedToEntity()
    {
        Material material = new Material(
            id: 1,
            name: "TestMaterial",
            count: 10,
            thresholdLimit: 5
        );
        
        var result = MaterialMapping.ModelToEntity(material);

        Assert.NotNull(result);
        Assert.IsType<DataAccess.Entities.Material>(result);
        Assert.Equal(0, result.Id);
        Assert.Equal(material.Name, result.Name);
        Assert.Equal(material.Count, result.Count);
        Assert.Equal(material.ThresholdLimit, result.ThresholdLimit);
    }
    
    [Fact]
    private void EntityShouldConvertedToModel()
    {
        DataAccess.Entities.Material material = new DataAccess.Entities.Material
        {
            Id = 1,
            Name = "TestMaterial",
            Count = 10,
            ThresholdLimit = 5
        };
        
        var result = MaterialMapping.EntityToModel(material);

        Assert.NotNull(result);
        Assert.IsType<Material>(result);
        Assert.Equal(material.Id, result.Id);
        Assert.Equal(material.Name, result.Name);
        Assert.Equal(material.Count, result.Count);
        Assert.Equal(material.ThresholdLimit, result.ThresholdLimit);
    }
}