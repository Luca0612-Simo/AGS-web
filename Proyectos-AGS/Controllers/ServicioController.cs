using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("AGS/[controller]")]
[ApiController]
public class ServicioController : ControllerBase
{
    private readonly IServicioService _service;

    public ServicioController(IServicioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetServicios()
    {
        var servicios = await _service.GetServicios();
        return Ok(servicios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _service.GetByIdServicio(id);
        if (servicio == null) return NotFound(new { message = "Servicio no encontrado" });
        return Ok(servicio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateServicio([FromForm] ServicioCreateDTO dto)
    {
        var nuevo = await _service.AddServicio(dto);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.id }, nuevo);
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateServicio(int id, [FromForm] ServicioUpdateDTO dto)
    {
        var result = await _service.UpdateServicio(id, dto);
        if (!result.Result) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteServicio(int id)
    {
        var result = await _service.DeleteServicio(id);
        if (!result.Result) return NotFound(result);
        return Ok(result);
    }
}