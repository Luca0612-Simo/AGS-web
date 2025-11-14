using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_models.DTO
{
    public class UserProfileDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public string requiere_cambio_contrasena { get; set; }
    }
}
