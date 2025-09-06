using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;
using BCrypt.Net;

namespace ProjectManagement.Application.Features.Users.Commands;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<UserDto>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Hash the password before saving
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        // Persist user using repository
        await _userRepository.AddAsync(user,cancellationToken);

        // Return DTO
        return new UserDto(user.Id, user.Username, user.Email, user.PasswordHash);
    }
}
