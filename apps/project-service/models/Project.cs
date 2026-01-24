namespace project_service.models;

public class Project
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
}
