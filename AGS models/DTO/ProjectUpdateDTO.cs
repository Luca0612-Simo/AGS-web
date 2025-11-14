using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AGS_Models.DTO
{
    public class ProjectUpdateDTO
    {
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(255)]
        public string descripcion { get; set; }

        public IFormFile? imagenFile { get; set; }
    }
}