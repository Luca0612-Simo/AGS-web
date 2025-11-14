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

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var proyectos = await _projectService.GetProjects();
        return Ok(proyectos);
    }

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

    [HttpPost]
    [Authorize] 
    public async Task<IActionResult> CreateProject([FromForm] ProjectCreateDTO projectDto)
    {
  
        var nuevoProyecto = await _projectService.CreateProject(projectDto);
        return CreatedAtAction(nameof(GetProjectById), new { id = nuevoProyecto.id }, nuevoProyecto);
    }

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