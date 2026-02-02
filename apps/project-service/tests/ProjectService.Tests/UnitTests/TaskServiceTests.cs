using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;
using project_service.Services;
using ProjectService.Tests.Fixtures;

namespace ProjectService.Tests.UnitTests;

[Collection("PostgresCollection")]
public class TaskServiceTests : IDisposable
{
  private readonly ProjectContext _context;

  private readonly TasksService _service;

  public TaskServiceTests(PostgresFixture fixture)
  {
    var options = new DbContextOptionsBuilder<ProjectContext>()
        .UseNpgsql(fixture.ConnectionString)
        .Options;

    _context = new ProjectContext(options);
    _context.Database.EnsureCreated();

    _service = new TasksService(_context);
  }

  private async Task<Column> SeedColumnAsync()
  {
    var project = new Project { Name = "Seed Project" };
    _context.Projects.Add(project);

    var column = new Column { Name = "Seed Column", ProjectId = project.Id };
    _context.Columns.Add(column);

    await _context.SaveChangesAsync();
    return column;
  }

  private async Task<Column> SeedTaskAsync()
  {
    var project = new Project { Name = "Seed Project" };
    _context.Projects.Add(project);

    var column = new Column { Name = "Seed Column", ProjectId = project.Id };
    _context.Columns.Add(column);

    var task = new ProjectTask { Title = "Seed task", ColumnId = column.Id };

    await _context.SaveChangesAsync();
    return column;
  }

  [Fact]
  public async Task CreateTaskAsync_ShouldReturnTask_WhenValidParameters()
  {
    // Given
    var column = await SeedColumnAsync();

    var dto = new CreateTaskDto
    {
      Title = "task1",
    };

    // When
    var task = await _service.CreateTaskAsync(column.Id, dto);

    // Then
    Assert.NotNull(task);
    Assert.Equal(task.Title, dto.Title);
    Assert.Equal(task.ColumnId, column.Id);
  }

  [Fact]
  public async Task GetTaskAsync_ShouldReturnTask_WhenExists()
  {
    // Given
    var project = new Project { Name = "project1" };
    _context.Projects.Add(project);

    var column = new Column { Name = "column1", ProjectId = project.Id };
    _context.Columns.Add(column);

    var task = new ProjectTask { Title = "task1", ColumnId = column.Id };
    _context.ProjectTasks.Add(task);

    await _context.SaveChangesAsync();

    // When
    var result = await _service.GetTaskAsync(task.Id);

    // Then
    Assert.NotNull(result);
    Assert.Equal(result.Id, task.Id);
  }

  [Fact]
  public async Task DeleteTaskAsync_ShouldReturnTrue_WhenDeleted()
  {
    // Given
    var project = new Project { Name = "project1" };
    _context.Projects.Add(project);

    var column = new Column { Name = "column1", ProjectId = project.Id };
    _context.Columns.Add(column);

    var task = new ProjectTask { Title = "task1", ColumnId = column.Id };
    _context.ProjectTasks.Add(task);

    await _context.SaveChangesAsync();

    // When
    var result = await _service.DeleteTaskAsync(task.Id);

    // Then
    Assert.True(result);
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}