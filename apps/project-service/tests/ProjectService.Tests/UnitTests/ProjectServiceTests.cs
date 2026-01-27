using Microsoft.EntityFrameworkCore;
using project_service.Services;
using project_service.Models;
using project_service.Data;
using project_service.Dtos;

namespace ProjectService.Tests.UnitTests;

public class ProjectServiceTests : IDisposable
{
    private readonly ProjectContext _context;
    private readonly ProjectsService _service;

    public ProjectServiceTests()
    {
        var options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ProjectContext(options);
        _service = new ProjectsService(_context);
    }

    [Fact]
    public async Task CreateProjectAsync_ShouldCreateAndReturnProject()
    {
        // Arrange
        var dto = new CreateProjectDTO { Name = "Test Project" };

        // Act
        var result = await _service.CreateProjectAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Project", result.Name);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task GetProject_ShouldReturnProject_WhenExists()
    {
        // Arrange
        var dto = new CreateProjectDTO { Name = "Test Project" };
        var createdProject = await _service.CreateProjectAsync(dto);

        // Act
        var result = await _service.GetProject(createdProject.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdProject.Id, result.Id);
        Assert.Equal("Test Project", result.Name);
    }

    [Fact]
    public async Task GetProject_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _service.GetProject(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}