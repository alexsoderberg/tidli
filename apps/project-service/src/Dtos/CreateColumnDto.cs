using System.ComponentModel.DataAnnotations;
using project_service.Models;

namespace project_service.Dtos;

public class CreateColumnDto {
  [Required(ErrorMessage = "A column requires a name")]
  [StringLength(100, MinimumLength = 3)]
  public required string Name;

  public ColumnType Type;
}