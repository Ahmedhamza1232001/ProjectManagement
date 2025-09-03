using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Users;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var user = await _userService.RegisterAsync(dto);
        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(dto.Email);
        if (user is null || !_authService.VerifyPassword(dto.Password, user.PasswordHash))
        {
            return Unauthorized();
        }

        var tokens =  _authService.GenerateTokens(user, cancellationToken);
        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(dto.UserId);
        if (user is null) return Unauthorized();

        var valid = await _authService.ValidateRefreshTokenAsync(user, dto.RefreshToken, cancellationToken);
        if (!valid) return Unauthorized();

        var tokens =  _authService.GenerateTokens(user, cancellationToken);
        return Ok(tokens);
    }
}
