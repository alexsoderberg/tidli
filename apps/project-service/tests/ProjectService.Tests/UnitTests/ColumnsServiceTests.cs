using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Dtos;
using project_service.Models;
using project_service.Services;

namespace ProjectService.Tests.UnitTests;

public class ColumnsServiceTests : IDisposable
{

  private readonly ProjectContext _context;
  private readonly ColumnsService _service;

  public ColumnsServiceTests()
  {
    var options = new DbContextOptionsBuilder<ProjectContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    _context = new ProjectContext(options);
    _service = new ColumnsService(_context);
  }

  [Fact]
  public async Task CreateColumnAsync_ShouldReturnColumn_WhenValidParameters()
  {
    // Given
    var project = new Project
    {
      Id = Guid.NewGuid(),
      Name = "project1"
    };

    _context.Projects.Add(project);
    await _context.SaveChangesAsync();
    


    var dto = new CreateColumnDto
    {
      Name = "column1",
      Type = ColumnType.Backlog
    };

    // When
    var column = await _service.CreateColumnAsync(project.Id, dto);

    // Then
    Assert.NotNull(column);
    Assert.Equal(column.ProjectId, project.Id);
    Assert.Equal(column.Name, dto.Name);
  }

  [Fact]
  public async Task GetColumnsAsync_ShouldReturnColumns_WhenColumnExist()
  {
    // 1. Arrange (Given)
    Guid projectId = Guid.NewGuid();

    _context.Projects.Add(new Project { Id = projectId, Name = "Test Project" });
    await _context.SaveChangesAsync();

    var dto = new CreateColumnDto
    {
      Name = "abc123",
      Type = ColumnType.Backlog
    };

    // 2. Act (When)
    var column = await _service.CreateColumnAsync(projectId, dto);
    var columns = await _service.GetColumnsAsync(projectId);

    // 3. Assert (Then)
    Assert.NotNull(columns);
    Assert.Single(columns);

    var result = columns[0];
    Assert.Equal(column.Id, result.Id);
    Assert.Equal(result.ProjectId, projectId);
    Assert.Equal(result.Name, dto.Name);
  }

  [Fact]
  public async Task GetColumnsAsync_ShouldReturnEmptyArray_WhenEmpty()
  {
    // Given
    Guid projectId = Guid.NewGuid();

    // Then
    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
      _service.GetColumnsAsync(projectId));

    Assert.Contains(projectId.ToString(), exception.Message);
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}