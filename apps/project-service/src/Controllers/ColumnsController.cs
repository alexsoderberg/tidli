using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project_service.Dtos;
using project_service.Models;
using project_service.Services;

namespace project_service.Controllers;

[ApiController]
[Route("api/v1/[controller]/{projectId}/columns")]
public class ColumnsController(ColumnsService service) : ControllerBase
{

  [HttpGet]
  public async Task<ActionResult<List<Column>>> GetColumns([FromRoute] Guid projectId)
  {
    var columns = await service.GetColumnsAsync(projectId);
    return Ok(columns);
  }

  [HttpPost]
  public async Task<ActionResult<Column>> CreateColumn([FromRoute] Guid projectId, CreateColumnDto dto)
  {
    var column = await service.CreateColumnAsync(projectId, dto);

    if (column == null)
    {
      return BadRequest();
    }

    return Ok(column);
  }
}