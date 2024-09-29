using System.ComponentModel.DataAnnotations;

namespace FloAPI.ViewModels.Material;

public class MaterialAddViewModel
{
    [Required(ErrorMessage = "Material name must be specified.")]
    [MaxLength(255, ErrorMessage = "Max length 255 characters.")]
    [MinLength(1, ErrorMessage = "Material name must be specified.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Material count must be specified.")]
    [Range(0, int.MaxValue, ErrorMessage = "Material count must be 0 or greater than 0.")]
    public int Count { get; set; }
    
    [Required(ErrorMessage = "Material threshold limit must be specified.")]
    [Range(0, int.MaxValue, ErrorMessage = "Material threshold limit must be 0 or greater than 0.")]
    public int ThresholdLimit { get; set; }
}