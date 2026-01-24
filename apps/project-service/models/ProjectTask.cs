namespace project_service.models;

public class ProjectTask
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public Guid ColumnId { get; set; }
  public required string Title { get; set; }
  public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
  public DateTime? TimerStartedAt { get; set; }
  public bool IsTimerRunning => TimerStartedAt.HasValue;
}
