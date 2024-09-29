namespace FloAPI.Business.Models;

public class Material
{
    public Material(
        int id,
        string name,
        int count,
        int thresholdLimit
        )
    {
        Id = id;
        Name = name;
        Count = count;
        ThresholdLimit = thresholdLimit;
    }
    
    public Material(
        string name,
        int count,
        int thresholdLimit
    )
    {
        Name = name;
        Count = count;
        ThresholdLimit = thresholdLimit;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public int ThresholdLimit { get; set; }
}