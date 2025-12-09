using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyectos_AGS.Controllers
{
    [Route("AGS/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _service;

        public EventoController(IEventoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene la lista de todos los eventos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            var eventos = await _service.GetAllEventos();
            return Ok(eventos);
        }

        /// <summary>
        /// Crea un nuevo evento.
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvento([FromBody] EventoCreateDTO dto)
        {
            var nuevo = await _service.CreateEvento(dto);
            return Ok(nuevo);
        }

        /// <summary>
        /// Actualiza un evento 
        /// </summary>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] EventoUpdateDTO dto)
        {
            var res = await _service.UpdateEvento(id, dto);

            if (!res.Result)return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Elimina un evento.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var res = await _service.DeleteEvento(id);

            if (!res.Result) return NotFound(res);
            return Ok(res);
        }
    }
}