using System.ComponentModel.DataAnnotations;

namespace FloAPI.ViewModels.Barcode;

public class BarcodeUpdateViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "For decrease value only positive number allowed.")]
    public int NumberOfDecrease { get; set; }
    
    [Required]
    public int MaterialId { get; set; }
}