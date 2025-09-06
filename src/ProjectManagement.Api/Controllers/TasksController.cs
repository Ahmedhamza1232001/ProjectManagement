using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProjectManagement.Application.DTOs.Tasks;
using ProjectManagement.Application.Features.Tasks.Commands;
using ProjectManagement.Application.Features.Tasks.Queries;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id), cancellationToken);
        return task is not null ? Ok(task) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _mediator.Send(new GetTasksQuery(), cancellationToken);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(new CreateTaskCommand(dto.Title, dto.Description, dto.ProjectId, dto.AssignedTo), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
    //need to be fixed 
    
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto, CancellationToken cancellationToken)
    // {
    //     var updated = await _mediator.Send(new UpdateTaskCommand(id, dto.Title, dto.Description, dto.TaskStatus, dto.Priority), cancellationToken);
    //     return updated is not null ? NoContent() : NotFound();
    // }

    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    // {
    //     var deleted = await _mediator.Send(new DeleteTaskCommand(id), cancellationToken);
    //     return deleted ? NoContent() : NotFound();
    // }
}
