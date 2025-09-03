using MediatR;
using ProjectManagement.Application.DTOs.Roles;
using ProjectManagement.Application.Exceptions;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Users.Commands;

public record AssignRoleCommand(Guid UserId, Guid RoleId) : IRequest<RoleDto>;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, RoleDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AssignRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException(nameof(User), request.UserId);

        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            throw new NotFoundException(nameof(Role), request.RoleId);

        user.Roles ??= new List<Role>();
        user.Roles.Add(role);

        await _userRepository.UpdateAsync(user);

        return new RoleDto(role.Id, role.Name ?? string.Empty);
    }
}
