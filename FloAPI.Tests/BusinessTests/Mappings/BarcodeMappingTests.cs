using FloAPI.Business.Mappings;
using FloAPI.Business.Models;

namespace FloAPI.Tests.BusinessTests.Mappings;

public class BarcodeMappingTests
{
    [Fact]
    private void ModelShouldConvertedToEntity()
    {
        Barcode barcode = new Barcode(
            id: 1,
            value: 9999999999999999,
            numberOfDecrease:5,
            materialId:1
        );
        
        var result = BarcodeMapping.ModelToEntity(barcode);

        Assert.NotNull(result);
        Assert.IsType<DataAccess.Entities.Barcode>(result);
        Assert.Equal(0, result.Id);
        Assert.Equal(barcode.Value, result.Value);
        Assert.Equal(barcode.NumberOfDecrease, result.NumberOfDecrease);
        Assert.Equal(barcode.MaterialId, result.MaterialId);
    }
    
    [Fact]
    private void ModelShouldConvertedToEntityWithId()
    {
        Barcode barcode = new Barcode(
            id: 1,
            value: 9999999999999999,
            numberOfDecrease:5,
            materialId:1
        );
        
        var result = BarcodeMapping.ModelToEntityWithId(barcode);

        Assert.NotNull(result);
        Assert.IsType<DataAccess.Entities.Barcode>(result);
        Assert.Equal(barcode.Id, result.Id);
        Assert.Equal(barcode.Value, result.Value);
        Assert.Equal(barcode.NumberOfDecrease, result.NumberOfDecrease);
        Assert.Equal(barcode.MaterialId, result.MaterialId);
    }
    
    [Fact]
    private void EntityShouldConvertedToModel()
    {
        DataAccess.Entities.Barcode barcode = new DataAccess.Entities.Barcode()
        {
            Id = 1,
            Value = 9999999999999999,
            NumberOfDecrease = 5,
            MaterialId = 1
        };
        
        var result = BarcodeMapping.EntityToModel(barcode);

        Assert.NotNull(result);
        Assert.IsType<Barcode>(result);
        Assert.Equal(barcode.Id, result.Id);
        Assert.Equal(barcode.Value, result.Value);
        Assert.Equal(barcode.NumberOfDecrease, result.NumberOfDecrease);
        Assert.Equal(barcode.MaterialId, result.MaterialId);
    }
}