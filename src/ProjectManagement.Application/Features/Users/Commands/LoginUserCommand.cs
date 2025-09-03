using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Users.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<UserDto>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null || user.PasswordHash != request.Password) // ⚠️ Replace with hashing in real life
            throw new UnauthorizedException("Invalid email or password");

        return new UserDto(user.Id, user.Username ?? string.Empty, user.Email ?? string.Empty, user.PasswordHash);
    }
}
