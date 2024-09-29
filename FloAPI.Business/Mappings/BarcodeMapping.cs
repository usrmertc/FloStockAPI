using FloAPI.Business.Models;

namespace FloAPI.Business.Mappings;

public class BarcodeMapping
{
    public static Barcode EntityToModel(FloAPI.DataAccess.Entities.Barcode barcode)
    {
        return new Barcode(
            id: barcode.Id,
            value: barcode.Value,
            numberOfDecrease: barcode.NumberOfDecrease,
            materialId: (int)barcode.MaterialId!
        );
    }
    
    public static FloAPI.DataAccess.Entities.Barcode ModelToEntity(Barcode barcode)
    {
        return new FloAPI.DataAccess.Entities.Barcode
        {
            Value = barcode.Value,
            NumberOfDecrease = barcode.NumberOfDecrease,
            MaterialId = barcode.MaterialId
        };
    }
    
    public static FloAPI.DataAccess.Entities.Barcode ModelToEntityWithId(Barcode barcode)
    {
        return new FloAPI.DataAccess.Entities.Barcode
        {
            Id = barcode.Id,
            Value = barcode.Value,
            NumberOfDecrease = barcode.NumberOfDecrease,
            MaterialId = barcode.MaterialId
        };
    }
}