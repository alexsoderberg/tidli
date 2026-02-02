using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;
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
  public async Task CreateTaskAsync_ShouldReturnTask_WhenValidParameters()
  {
    // Given
    var column = new Column
    {
      Name = "column1",
      ProjectId = Guid.NewGuid(),
    };

    var columnId = column.Id;

    _context.Columns.Add(column);
    await _context.SaveChangesAsync();

    var dto = new CreateTaskDto
    {
      Title = "task1",

    };
    // When
    var task = await _service.CreateTaskAsync(columnId, dto);

    // Then
    Assert.NotNull(task);
    Assert.Equal(task.Title, dto.Title);
    Assert.Equal(task.ColumnId, columnId);
  }

  [Fact]
  public async Task GetTaskAsync_ShouldReturnTask_WhenExists()
  {
    // Given
    var expectedId = Guid.NewGuid();
    var expected = new ProjectTask
    {
      Title = "task1",
      Id = expectedId
    };
    _context.ProjectTasks.Add(expected);

    await _context.SaveChangesAsync();
  
    // When
    var task = await _service.GetTaskAsync(expected.Id);
  
    // Then
    Assert.NotNull(task);
    Assert.Equal(expectedId, task.Id);
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}