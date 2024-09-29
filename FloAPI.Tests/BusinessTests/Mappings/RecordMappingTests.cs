using FloAPI.Business.Mappings;
using Record = FloAPI.Business.Models.Record;

namespace FloAPI.Tests.BusinessTests.Mappings;

public class RecordMappingTests
{
    [Fact]
    private void ModelShouldConvertedToEntity()
    {
        Record record = new Record(
            id: 1,
            count: 15,
            operationType: true,
            creationDate: DateTime.UtcNow,
            materialId: 1
            );
        
        var result = RecordMapping.ModelToEntity(record);

        Assert.NotNull(result);
        Assert.IsType<DataAccess.Entities.Record>(result);
        Assert.Equal(record.Count, result.Count);
        Assert.Equal(record.OperationType, result.OperationType);
        Assert.Equal(record.MaterialId, result.MaterialId);
    }
    
    [Fact]
    private void EntityShouldConvertedToModel()
    {
        DataAccess.Entities.Record record = new DataAccess.Entities.Record()
        {
            Id = 1,
            Count = 15,
            OperationType = true,
            MaterialId = 5,
        };
        
        var result = RecordMapping.EntityToModel(record);

        Assert.NotNull(result);
        Assert.IsType<Record>(result);
        Assert.Equal(record.Id, result.Id);
        Assert.Equal(record.Count, result.Count);
        Assert.Equal(record.OperationType, result.OperationType);
        Assert.Equal(record.MaterialId, result.MaterialId);
    }
}