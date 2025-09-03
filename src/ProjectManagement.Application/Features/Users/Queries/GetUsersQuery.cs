using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Users.Queries;

public record GetUsersQuery : IRequest<List<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users
            .Select(u => new UserDto(u.Id, u.Username ?? string.Empty, u.Email ?? string.Empty, u.PasswordHash))
            .ToList();
    }
}
