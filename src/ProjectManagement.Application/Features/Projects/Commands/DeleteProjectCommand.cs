using MediatR;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Commands;

public record DeleteProjectCommand(Guid Id) : IRequest<bool>;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id,cancellationToken);
        if (project is null)
            throw new NotFoundException($"Project with Id {request.Id} not found");

        await _projectRepository.DeleteAsync(project,cancellationToken);
        return true;
    }
}
