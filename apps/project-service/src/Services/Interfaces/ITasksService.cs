using project_service.Dtos;
using project_service.Models;

namespace project_service.Services.Interfaces;
public interface ITasksService
{
  Task<ProjectTask?> GetTaskAsync(Guid id);

  Task<ProjectTask> CreateTaskAsync(Guid columnId, CreateTaskDto dto);

  
}