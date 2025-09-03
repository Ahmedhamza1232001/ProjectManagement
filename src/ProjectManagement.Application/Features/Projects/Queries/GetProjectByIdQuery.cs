using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Queries;

public record GetProjectByIdQuery(Guid Id) : IRequest<ProjectDto>;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id,cancellationToken);
        if (project is null)
            throw new NotFoundException($"Project with Id {request.Id} not found");

        return new ProjectDto(project.Id, project.Name, project.Description?? string.Empty);
    }
}
