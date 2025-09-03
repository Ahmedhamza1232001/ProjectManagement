using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Tasks.Commands
{
    public record BulkUpdateTasksCommand(List<UpdateTaskDto> Tasks) : IRequest<List<TaskDto>>;

    public class BulkUpdateTasksCommandHandler : IRequestHandler<BulkUpdateTasksCommand, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public BulkUpdateTasksCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskDto>> Handle(BulkUpdateTasksCommand request, CancellationToken cancellationToken)
        {
            var updatedTasks = new List<TaskDto>();

            foreach (var dto in request.Tasks)
            {
                var task = await _taskRepository.GetByIdAsync(dto.Id);
                if (task != null)
                {
                    task.Title = dto.Title;
                    task.Description = dto.Description;
                    await _taskRepository.UpdateAsync(task);

                    updatedTasks.Add(new TaskDto(task.Id, task.Title, task.Description, task.ProjectId));
                }
            }

            return updatedTasks;
        }
    }
}
