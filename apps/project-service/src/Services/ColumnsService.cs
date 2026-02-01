using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;

namespace project_service.Services;

public class ColumnsService(ProjectContext context)
{
  public async Task<List<Column>> GetColumnsAsync(Guid projectId)
  {
    var columns = await context.Columns
      .Where(c => c.ProjectId == projectId)
      .OrderBy(c => c.Order)
      .ToListAsync();

    if (columns.Count == 0)
    {
      if (!await IsExistingProject(projectId))
      {
        throw new KeyNotFoundException($"Project {projectId} not found.");
      }
    }


    return [.. columns];
  }

  public async Task<Column> CreateColumnAsync(Guid projectId, CreateColumnDto dto)
  {
    if (!await IsExistingProject(projectId))
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

  private async Task<bool> IsExistingProject(Guid projectId)
  {
    var exists = await context.Projects.AnyAsync(p => p.Id == projectId);
    if (!exists)
    {
      return false;
    }
    return true;
  }
}