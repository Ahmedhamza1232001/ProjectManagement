using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProjectManagement.Application.DTOs.Projects;
using ProjectManagement.Application.Features.Projects.Commands;
using ProjectManagement.Application.Features.Projects.Queries;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);
        if (project is null) return NotFound();
        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var projects = await _mediator.Send(new GetProjectsQuery(), cancellationToken);
        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto request, CancellationToken cancellationToken)
    {
        var project = await _mediator.Send(new CreateProjectCommand(request.Name, request.Description), cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }
}
