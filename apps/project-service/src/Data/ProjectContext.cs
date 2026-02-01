using Microsoft.EntityFrameworkCore;
using project_service.Models;

namespace project_service.Data;

public class ProjectContext(DbContextOptions<ProjectContext> options) : DbContext(options)
{
  public DbSet<Project> Projects { get; set; } = null!;
  public DbSet<Column> Columns { get; set; } = null!;
  public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
}