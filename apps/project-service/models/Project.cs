namespace project_service.models;

public class Project
{
  private readonly Guid id;
  private TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
}
