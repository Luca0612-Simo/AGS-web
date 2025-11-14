using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // <-- AGREGÁ ESTE
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_Models
{
    public class Proyecto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        [NotMapped] 
        public string Url { get; set; }
    }
}