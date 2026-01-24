namespace project_service.models;

public class ProjectTask
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public string Title { get; set; }
  public Guid ColumnId { get; set; }
  public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
  public DateTime? TimerStartedAt { get; set; }
  public bool IsTimerRunning => TimerStartedAt.HasValue;
}
