using Microsoft.AspNetCore.Mvc;
using project_service.Dtos;
using project_service.Models;
using project_service.Services;

namespace project_service.controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController(ProjectsService service) : ControllerBase
{

  [HttpGet("{id}")]
  public async Task<ActionResult<Project>> GetProject(Guid id)
  {
    var project = await service.GetProject(id);
    if (project == null)
    {
      return NotFound();
    }
    return Ok(project);
  }

  [HttpPost]
  public async Task<ActionResult<Project>> CreateProject([FromBody] CreateProjectDTO dto)
  {
    var project = await service.CreateProjectAsync(dto);

    return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<Project>> DeleteProjectAsync(Guid id)
  {
    var isDeleted = await service.DeleteProjectAsync(id);
    
    if (!isDeleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}