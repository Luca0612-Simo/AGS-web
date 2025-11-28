using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_Models
{
    public class User
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public string contrasena { get; set; }
        public string telefono { get; set; }
        public string requiere_cambio_contrasena { get; set; }

        [Column("fechaAlta")]
        public DateOnly? fechaAlta { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow); 

        [Column("creadoPor")]
        public int? creadoPor { get; set; }

        [Column("fechaBaja")]
        public DateOnly? fechaBaja { get; set; }

        [Column("eliminadoPOr")]
        public int? eliminadoPor { get; set; }

        [Column("estaEliminado")]
        public bool estaEliminado { get; set; } = false;
    }
}