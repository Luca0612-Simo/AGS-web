using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AGS_Models.DTO
{
    public class ProjectCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string descripcion { get; set; }

        [Required]
        public DateOnly fecha_inicio { get; set; }

        [Required]
        public string estado { get; set; }

        [Required]
        public int horas { get; set; }

        [Required]
        public IFormFile imagenFile { get; set; }
    }
}