using project_service.Dtos;
using project_service.Models;

namespace project_service.Services.Interfaces;

public interface IColumnsService
{
  Task<List<Column>> GetColumnsAsync(Guid projectId);

  Task<Column> CreateColumnAsync(Guid projectId, CreateColumnDto dto);
  
}