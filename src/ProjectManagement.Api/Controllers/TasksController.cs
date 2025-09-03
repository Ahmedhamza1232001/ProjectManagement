using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Tasks;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id, CancellationToken cancellationToken)
    {
        var task =  _taskService.GetByIdAsync(id, cancellationToken);
        return task is not null ? Ok(task) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllAsync(cancellationToken);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateAsync(dto,cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto, CancellationToken cancellationToken)
    {
        var updated = await _taskService.UpdateAsync(dto, cancellationToken);
        return updated is not null ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
