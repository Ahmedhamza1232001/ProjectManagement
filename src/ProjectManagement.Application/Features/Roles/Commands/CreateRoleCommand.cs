using MediatR;
using ProjectManagement.Application.DTOs.Roles;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Roles.Commands;

public record CreateRoleCommand(string Name) : IRequest<RoleDto>;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role { Name = request.Name };
        await _roleRepository.AddAsync(role);

        return new RoleDto(role.Id, role.Name);
    }
}
