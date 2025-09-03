using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
        if (task is null) return null;

        return new TaskDto(task.Id, task.Title, task.Description??string.Empty, task.ProjectId);
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);
        return tasks.Select(t => new TaskDto(t.Id, t.Title, t.Description??string.Empty, t.ProjectId));
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            ProjectId = dto.ProjectId,
        };

        await _taskRepository.AddAsync(task, cancellationToken);

        return new TaskDto(task.Id, task.Title, task.Description, task.ProjectId);
    }

    public async Task<TaskDto?> UpdateAsync(UpdateTaskDto dto, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (task is null) return null;

        task.Title = dto.Title;
        task.Description = dto.Description;

        await _taskRepository.UpdateAsync(task, cancellationToken);

        return new TaskDto(task.Id, task.Title, task.Description, task.ProjectId);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
        if (task is null) return false;

        await _taskRepository.DeleteAsync(task, cancellationToken);
        return true;
    }
}
