using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloAPI.DataAccess.Entities
{
    [Table("barcodes")]
    public class Barcode
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("value")]
        [MaxLength(16, ErrorMessage = "Barcode value must be 16 characters long.")]
        [MinLength(16, ErrorMessage = "Barcode value must be 16 characters long.")]
        public Int64 Value { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        [Column("number_of_decrease")]
        public int NumberOfDecrease { get; set; }
        
        [Column("material_id")]
        public int? MaterialId { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}
