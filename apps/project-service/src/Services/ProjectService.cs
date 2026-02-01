using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;

namespace project_service.Services;

public class ProjectsService(ProjectContext context)
{
  private readonly ProjectContext _context = context;

  public async Task<Project?> GetProjectAsync(Guid id)
  {
    return await _context.Projects.FindAsync(id);
  }

  public async Task<Project> CreateProjectAsync(CreateProjectDTO dto)
  {
    var project = new Project
    {
      Name = dto.Name
    };
      
    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    return project;
  }

  public async Task<bool> DeleteProjectAsync(Guid id)
  {
    var deletedRows = await _context.Projects
        .Where(p => p.Id == id)
        .ExecuteDeleteAsync();

    return deletedRows > 0;
  }
}