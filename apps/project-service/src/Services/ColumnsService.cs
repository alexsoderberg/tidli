using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;

namespace project_service.Services;

public class ColumnsService (ProjectContext context) {
  public async Task<List<Column>> GetAllColumnsAsync(Guid projectId)
  {
    var columns = await context.Columns.ToListAsync();

    return columns;
  }

  public async Task<Column> CreateColumnAsync(Guid projectId, CreateColumnDto dto)
  {

    var projectExists = await context.Projects.AnyAsync(p => p.Id == projectId);
    if (!projectExists)
    {
        throw new KeyNotFoundException($"Project with ID {projectId} was not found.");
    }

    // Find highest order column in project
    var maxOrder = await context.Columns
        .Where(c => c.ProjectId == projectId)
        .Select(c => (int?)c.Order)
        .MaxAsync() ?? -1;

    // Create new column and place last in order
    var column = new Column
    {
        ProjectId = projectId,
        Name = dto.Name,
        Type = dto.Type,
        Order = maxOrder + 1
    };

    context.Columns.Add(column);
    await context.SaveChangesAsync();

    return column;
  }
}