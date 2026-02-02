using Microsoft.EntityFrameworkCore;
using project_service.Dtos;
using project_service.Models;

namespace project_service.Services.Interfaces;

public interface IProjectService
{
  Task<Project?> GetProjectAsync(Guid id);

  Task<Project> CreateProjectAsync(CreateProjectDTO dto);

  Task<bool> DeleteProjectAsync(Guid id);
}
