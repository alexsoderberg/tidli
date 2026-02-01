using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Services;

namespace ProjectService.Tests.UnitTests;

public class TaskServiceTests : IDisposable
{
  private readonly ProjectContext _context;

  private readonly TasksService _service;

  public TaskServiceTests()
  {
    var options = new DbContextOptionsBuilder<ProjectContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    _context = new ProjectContext(options);
    _service = new TasksService(_context);
  }

  [Fact]
  public void CreateTaskAsync_ShouldReturnTask_WhenValidParameters()
  {
    // Given
    // When
  
    // Then
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}