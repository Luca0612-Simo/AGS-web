using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_models.DTO
{
    public class UserCreateDTO
    {
        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        public string mail { get; set; }

        [Required]
        public string contrasena { get; set; }

        public string telefono { get; set; }

        [Required]
        public string requiere_cambio_contrasena { get; set; }
    }
}
