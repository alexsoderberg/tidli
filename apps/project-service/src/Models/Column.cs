namespace project_service.Models;

public class Column
{
  public Guid Id { get; init; } = Guid.NewGuid();

  public Guid ProjectId { get; set; }
  public required string Name { get; set; }
  public int Order { get; set; }

  public ColumnType Type { get; set; }
  
  public Project Project { get; set; } = null!;
  public List<ProjectTask> Tasks { get; set; } = new();
}