using project_service.Models;
using project_service.Data;
using project_service.Dtos;

namespace project_service.Services;

public class ProjectsService(ProjectContext context)
{
  private readonly ProjectContext _context = context;

  public async Task<Project?> GetProject(Guid id)
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
}