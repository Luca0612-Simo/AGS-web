using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGS_Models
{
    public class Proyecto
    {
        [Key]
        public int id { get; set; }

        [Column("nombre")] 
        public string nombre { get; set; }

        public string descripcion { get; set; }
        public string imagen { get; set; } 
        public DateOnly? fecha_inicio { get; set; } 
        public DateOnly? fecha_fin { get; set; }
        public string estado { get; set; }
        public int horas { get; set; }

        [NotMapped] 
        public string Url { get; set; }
    }
}