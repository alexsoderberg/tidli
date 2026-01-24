namespace project_service.models;

public class Column
{
  public Guid Id { get; init; } = Guid.NewGuid();

  public Guid ProjectId { get; set; }
  public required string Name { get; set; }
  public int Order { get; set; }

  public ColumnType Type { get; set; }
}