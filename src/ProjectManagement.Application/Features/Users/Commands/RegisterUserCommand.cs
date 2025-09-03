using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

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
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password // 🔒 (in real apps: hash this!)
        };

        await _userRepository.AddAsync(user);

        return new UserDto(user.Id, user.Username, user.Email,user.PasswordHash);
    }
}
