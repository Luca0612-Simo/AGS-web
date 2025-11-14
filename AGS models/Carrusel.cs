using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGS_models
{
    public class Carrusel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2048)] 
        public string ImageKey { get; set; }

        [StringLength(255)] 
        public string? Nombre { get; set; }

        public int Orden { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string Url { get; set; }
    }
}