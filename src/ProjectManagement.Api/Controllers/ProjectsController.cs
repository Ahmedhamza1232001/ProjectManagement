using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Projects;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectService.GetByIdAsync(id, cancellationToken);
        if (project is null) return NotFound();

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var projects = await _projectService.GetAllAsync(cancellationToken);
        var response = projects.Select(p => new ProjectDto(
        p.Id,
        p.Name,
        p.Description
        ));

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto request, CancellationToken cancellationToken)
    {
        var project = await _projectService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, new ProjectDto(
        project.Id,
        project.Name,
        project.Description
    ));
    }
}
