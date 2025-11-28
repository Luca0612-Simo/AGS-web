using AGS_Models.DTO;
using AGS_services;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("AGS/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Te devuelve todos los proyectos.
    /// </summary>
    /// <remarks>
    /// Genera URLs firmadas de S3 para las imágenes en tiempo real.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var proyectos = await _projectService.GetProjects();
        return Ok(proyectos);
    }

    /// <summary>
    /// Devuelve un proyecto por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var proyecto = await _projectService.GetByIdProject(id);
        if (proyecto == null)
        {
            return NotFound(new { message = "Proyecto no encontrado" });
        }
        return Ok(proyecto);
    }

    /// <summary>
    /// Crea un nuevo proyecto con imagen.
    /// </summary>
    [HttpPost]
    [Authorize] 
    public async Task<IActionResult> CreateProject([FromForm] ProjectCreateDTO projectDto)
    {
  
        var nuevoProyecto = await _projectService.CreateProject(projectDto);
        return CreatedAtAction(nameof(GetProjectById), new { id = nuevoProyecto.id }, nuevoProyecto);
    }

    /// <summary>
    /// Actualiza un proyecto existente.
    /// </summary>
    /// <remarks>
    /// Podes actualizar datos o reemplazar la imagen si se envía una nueva.
    /// </remarks>
    [HttpPatch("{id}")]
    [Authorize] 
    public async Task<IActionResult> UpdateProject(int id, [FromForm] ProjectUpdateDTO projectDto)
    {
        var result = await _projectService.UpdateProject(id, projectDto);
        if (!result.Result)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Finaliza un proyecto.
    /// </summary>
    /// <remarks>
    /// Cambia el estado a "Finalizado" y establece la fecha de fin a hoy, en lugar de eliminarlo.
    /// </remarks>
    [HttpDelete("{id}")]
    [Authorize] 
    public async Task<IActionResult> DeleteProyecto(int id)
    {
        var result = await _projectService.DeleteProject(id);
        if (!result.Result)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}