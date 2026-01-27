namespace project_service.Models;

public class Project
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public required string Name { get; set; }
  public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
}
