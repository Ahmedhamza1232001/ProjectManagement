using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Projects.Commands;

public record UpdateProjectCommand(Guid Id, string Name, string Description) : IRequest<ProjectDto>;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null)
            throw new NotFoundException($"Project with Id {request.Id} not found");

        project.Name = request.Name;
        project.Description = request.Description;

        await _projectRepository.UpdateAsync(project);

        return new ProjectDto(project.Id, project.Name, project.Description);
    }
}
