using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Tasks.Queries
{
    public record GetTasksQuery : IRequest<List<TaskDto>>;

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(t => new TaskDto(t.Id, t.Title, t.Description??string.Empty, t.ProjectId)).ToList();
        }
    }
}
