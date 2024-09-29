namespace FloAPI.Business.Models;

public class Barcode
{
    public Barcode(
        int id, 
        Int64 value, 
        int numberOfDecrease, 
        int materialId
    )
    {
        Id = id;
        Value = value;
        NumberOfDecrease = numberOfDecrease;
        MaterialId = materialId;
    }
    
    public Barcode(
        int id, 
        int numberOfDecrease, 
        int materialId
    )
    {
        Id = id;
        NumberOfDecrease = numberOfDecrease;
        MaterialId = materialId;
    }

    public Barcode(
        int numberOfDecrease, 
        int materialId
    )
    {
        NumberOfDecrease = numberOfDecrease;
        MaterialId = materialId;
    }
    
    public int Id { get; set; }
    public Int64 Value { get; set; }
    public int NumberOfDecrease { get; set; }
    public int MaterialId { get; set; }
}