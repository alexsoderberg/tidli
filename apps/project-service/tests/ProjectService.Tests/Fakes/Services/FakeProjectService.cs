using project_service.Dtos;
using project_service.Models;
using project_service.Services.Interfaces;

namespace ProjectService.Tests.Fakes.Services;

public class FakeProjectService : IProjectService
{

  public Project? ProjectToReturn { get; set; } = new Project { Name = "FakeProject"};
  public bool DeleteResult { get; set; } = true;
  public Project? LastCreatedProject { get; private set; }


  public Task<Project> CreateProjectAsync(CreateProjectDTO dto)
  {
    return Task.FromResult(ProjectToReturn)!;
  }

  public Task<bool> DeleteProjectAsync(Guid id)
  {
    return Task.FromResult(DeleteResult);
  }

  public Task<Project?> GetProjectAsync(Guid id)
  {
    return Task.FromResult(ProjectToReturn);
  }
}