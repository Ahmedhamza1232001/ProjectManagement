using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Application.Abstractions.Repositories;
namespace ProjectManagement.Application.Features.Projects.Commands;

public record CreateProjectCommand(string Name, string Description) : IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description
        };
        await _projectRepository.AddAsync(project,cancellationToken);

        return new ProjectDto(project.Id, project.Name, project.Description);
    }
}