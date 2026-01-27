using System.ComponentModel.DataAnnotations;

namespace project_service.Dtos;
public class CreateProjectDTO
{
  [Required(ErrorMessage = "A project requires a name")]
  [StringLength(100, MinimumLength = 3)]
  public required string Name { get; set; }
}