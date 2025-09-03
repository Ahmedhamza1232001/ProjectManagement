using MediatR;
using ProjectManagement.Application.DTOs.Roles;
using ProjectManagement.Application.Abstractions.Repositories;

namespace ProjectManagement.Application.Features.Roles.Queries;

public record GetRolesQuery : IRequest<List<RoleDto>>;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => new RoleDto(r.Id, r.Name)).ToList();
    }
}
