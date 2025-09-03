using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Queries;

public record GetProjectsQuery : IRequest<List<ProjectDto>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);
        return projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description??string.Empty)).ToList();
    }
}
