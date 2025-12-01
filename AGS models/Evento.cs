using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGS_Models
{
    public class Evento
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        public int horas { get; set; }
    }
}