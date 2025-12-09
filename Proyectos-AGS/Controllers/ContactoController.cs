using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Proyectos_AGS.Controllers
{
    [Route("AGS/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactoController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Envia el formulario al mail de destino.
        /// </summary>
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensaje([FromBody] ContactoDTO contactoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _emailService.EnviarCorreoContacto(contactoDto);

            if (resultado)
            {
                return Ok(new { message = "Mensaje enviado correctamente.Nos pondremos en contacto pronto." });
            }
            else
            {
                return StatusCode(500, new { message = "Hubo un error al enviar el mensaje.Intente más tarde." });
            }
        }
    }
}