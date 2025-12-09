using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AGS_Models.DTO
{
    public class ServicioCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string descripcion { get; set; }

    }
}