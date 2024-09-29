using System.ComponentModel.DataAnnotations;

namespace FloAPI.ViewModels.Material;

public class MaterialGetViewModel
{
    [Required]
    [MaxLength(255, ErrorMessage = "Max length 255 characters.")]
    public string Name { get; set; } = "";
}