using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;
using project_service.Services.Interfaces;

namespace project_service.Services;

public class TasksService(ProjectContext _context) : ITasksService
{
  public async Task<ProjectTask?> GetTaskAsync(Guid id)
  {
    var projectTask = await _context.ProjectTasks.FindAsync(id);

    return projectTask;
  }

  public async Task<ProjectTask> CreateTaskAsync(Guid columnId, CreateTaskDto dto)
  {
    var projectTask = new ProjectTask
    {
      ColumnId = columnId,
      Title = dto.Title
    };

    _context.ProjectTasks.Add(projectTask);
    await _context.SaveChangesAsync();

    return projectTask;
  }
}
