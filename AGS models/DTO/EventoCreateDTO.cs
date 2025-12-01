using System.ComponentModel.DataAnnotations;

namespace AGS_Models.DTO
{
    public class EventoCreateDTO
    {
        [Required]
        public string nombre { get; set; }

        [Required]
        public int horas { get; set; }
    }
}