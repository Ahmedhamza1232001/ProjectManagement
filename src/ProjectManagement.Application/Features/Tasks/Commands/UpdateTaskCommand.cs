using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.Commands
{
    public record UpdateTaskCommand(Guid Id, string Title, string? Description)
        : IRequest<TaskDto>;

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
            if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

            task.Title = request.Title;
            task.Description = request.Description;

            await _taskRepository.UpdateAsync(task,cancellationToken);

            return new TaskDto(task.Id, task.Title, task.Description??string.Empty, task.ProjectId);
        }
    }
}
