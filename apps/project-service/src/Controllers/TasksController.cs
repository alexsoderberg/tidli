using Microsoft.AspNetCore.Mvc;
using project_service.Dtos;
using project_service.Models;
using project_service.Services;

namespace project_service.Controllers;

[ApiController]
[Route("api/v1/{projectId}/{columnId}/[controller]")]
public class TasksController(TasksService _service) : ControllerBase
{

  [HttpGet]
  [Route("/{id}")]
  public async Task<ActionResult<ProjectTask>> GetTask(Guid id)
  {
    var projektTask = await _service.GetTaskAsync(id);

    if (projektTask == null)
    {
      return NotFound();
    }

    return Ok(projektTask);
  }

  [HttpPost]
  public async Task<ActionResult<ProjectTask>> CreateTask([FromRoute] Guid columnId, [FromBody] CreateTaskDto dto)
  {


    var projectTask = await _service.CreateTaskAsync(columnId, dto);

    if (projectTask == null)
    {
      return BadRequest();
    }

    return CreatedAtAction(nameof(GetTask), new { id = projectTask.Id }, projectTask);
  }
}