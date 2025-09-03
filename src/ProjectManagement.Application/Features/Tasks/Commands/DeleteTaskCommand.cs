using System.IO.Pipes;
using MediatR;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.Commands
{
    public record DeleteTaskCommand(Guid Id) : IRequest;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id,cancellationToken);
            if (task == null) throw new NotFoundException(nameof(TaskItem), request.Id);

            await _taskRepository.DeleteAsync(task,cancellationToken);
        }
    }
}
