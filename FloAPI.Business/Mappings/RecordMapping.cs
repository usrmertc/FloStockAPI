using FloAPI.Business.Models;

namespace FloAPI.Business.Mappings;

public static class RecordMapping
{
    public static Record EntityToModel(FloAPI.DataAccess.Entities.Record record)
    {
        return new Record(
            id: record.Id,
            count: record.Count,
            operationType: record.OperationType,
            creationDate: record.Date,
            materialId: (int)record.MaterialId!
        );
    }
    
    public static FloAPI.DataAccess.Entities.Record ModelToEntity(Record record)
    {
        return new FloAPI.DataAccess.Entities.Record
        {
            Count = record.Count, 
            OperationType = record.OperationType, 
            MaterialId = record.MaterialId
        };
    }
}