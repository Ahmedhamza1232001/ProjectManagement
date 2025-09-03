// ITaskService.cs
using ProjectManagement.Application.DTOs.Tasks;

namespace ProjectManagement.Application.Abstractions.Services;

public interface ITaskService
{
    Task<TaskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken);
    Task<TaskDto> UpdateAsync(UpdateTaskDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id,CancellationToken cancellationToken);
}
