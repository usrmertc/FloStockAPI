using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FloAPI.DataAccess.Entities
{
    [Table("materials")]
    public class Material
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Unicode]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only zero or positive number allowed.")]
        [Column("count")]
        public int Count { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only zero or positive number allowed.")]
        [Column("threshold_limit")]
        public int ThresholdLimit { get; set; }

        public Record Record { get; set; }
        public Barcode Barcode { get; set; }
    }
}
