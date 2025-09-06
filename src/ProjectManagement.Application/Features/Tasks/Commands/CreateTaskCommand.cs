using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.Commands;

public record CreateTaskCommand(string Title, string Description, Guid ProjectId, User AssignedTo) : IRequest<TaskDto>;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            ProjectId = request.ProjectId,
            AssignedTo = request.AssignedTo
        };

        await _taskRepository.AddAsync(task,cancellationToken);

        return new TaskDto(task.Id, task.Title, task.Description, task.ProjectId);
    }
}
