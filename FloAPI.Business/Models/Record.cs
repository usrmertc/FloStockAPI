namespace FloAPI.Business.Models;

public class Record
{
    public Record(
        int count,
        bool operationType,
        int materialId
    )
    {
        Count = count;
        OperationType = operationType;
        MaterialId = materialId;
    }
    public Record(
        int id,
        int count,
        bool operationType,
        DateTime creationDate,
        int materialId
        )
    {
        Id = id;
        Count = count;
        OperationType = operationType;
        CreationDate = creationDate;
        MaterialId = materialId;
    }
    
    public int? Id { get; set; }
    public int Count { get; set; }
    public bool OperationType { get; set; }
    public DateTime? CreationDate { get; set; }
    public int MaterialId { get; set; }
}