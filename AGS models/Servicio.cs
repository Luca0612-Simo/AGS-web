using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGS_Models
{
    public class Servicio
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(2048)]
        public string imagen { get; set; } 

        [NotMapped]
        public string Url { get; set; } 
    }
}