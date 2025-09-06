using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Features.Users.Commands;
using ProjectManagement.Application.Features.Users.Queries;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new RegisterUserCommand(dto.Username, dto.Email, dto.Password), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
    // need to be fixed 
    
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    // {
    //     var updated = await _mediator.Send(new UpdateUserCommand(id, dto.Username, dto.Email), cancellationToken);
    //     return updated is not null ? NoContent() : NotFound();
    // }

    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    // {
    //     await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
    //     return NoContent();
    // }
}
