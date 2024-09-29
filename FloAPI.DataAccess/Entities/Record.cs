using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloAPI.DataAccess.Entities
{
    [Table("records")]
    public class Record
    {
        
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("count")]
        public int Count { get; set; }

        [Required]
        [Column("operation_type")]
        public bool OperationType { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Column("material_id")]
        public int? MaterialId { get; set; }
        public ICollection<Material> Materials { get; set; }
    }


}
