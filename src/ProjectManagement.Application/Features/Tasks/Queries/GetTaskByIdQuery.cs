using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Domain.Entities;
namespace ProjectManagement.Application.Features.Tasks.Queries
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto>;

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id,cancellationToken);
            if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

            return new TaskDto(task.Id, task.Title, task.Description??string.Empty, task.ProjectId);
        }
    }
}
