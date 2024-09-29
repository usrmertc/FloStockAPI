using System.ComponentModel.DataAnnotations;

namespace FloAPI.ViewModels.Material;

public class MaterialThresholdLimitUpdateViewModel
{
    [Required(ErrorMessage = "Material threshold limit must be specified.")]
    [Range(0, int.MaxValue, ErrorMessage = "Material threshold limit must be 0 or greater than 0.")]
    public int ThresholdLimit { get; set; }
}