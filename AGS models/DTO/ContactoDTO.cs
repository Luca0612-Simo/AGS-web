using System.ComponentModel.DataAnnotations;

namespace AGS_Models.DTO
{
    public class ContactoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El tipo de proyecto es obligatorio")]
        public string TipoProyecto { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string Mensaje { get; set; }
    }
}