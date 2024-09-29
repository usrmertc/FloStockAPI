using System.ComponentModel.DataAnnotations;

namespace FloAPI.ViewModels.Barcode;

public class BarcodeGetAndConsumeViewModel
{
    [Required]
    public Int64 Value { get; set; }
}