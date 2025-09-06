using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Features.Users.Commands;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var user = await _mediator.Send(new RegisterUserCommand(dto.Username, dto.Email,dto.Password));
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        // Send a query or command to handle login
        var tokens = await _mediator.Send(new LoginUserCommand(dto.Email, dto.Password), cancellationToken);
        if (tokens is null)
            return Unauthorized();

        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        // Send a command to handle token refresh
        var tokens = await _mediator.Send(new RefreshTokensCommand(dto.UserId, dto.RefreshToken), cancellationToken);
        if (tokens is null)
            return Unauthorized();

        return Ok(tokens);
    }
}
