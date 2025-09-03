using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Tasks.Queries
{
    public record SearchTasksQuery(string Keyword) : IRequest<List<TaskDto>>;

    public class SearchTasksQueryHandler : IRequestHandler<SearchTasksQuery, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public SearchTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskDto>> Handle(SearchTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.SearchAsync(request.Keyword);
            return tasks.Select(t => new TaskDto(t.Id, t.Title, t.Description??string.Empty, t.ProjectId)).ToList();
        }
    }
}
