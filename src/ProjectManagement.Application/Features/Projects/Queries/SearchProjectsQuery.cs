using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Queries;

public record SearchProjectsQuery(string Keyword) : IRequest<List<ProjectDto>>;

public class SearchProjectsQueryHandler : IRequestHandler<SearchProjectsQuery, List<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public SearchProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.SearchAsync(request.Keyword, cancellationToken);
        return projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description??string.Empty)).ToList();
    }
}
