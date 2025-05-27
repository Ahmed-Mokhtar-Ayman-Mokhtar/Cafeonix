using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cafeonix.Models
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(255)]
        public string Category { get; set; }
    }
}