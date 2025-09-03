using MediatR;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Exceptions;

namespace ProjectManagement.Application.Features.Roles.Commands;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);
        if (role is null)
            throw new NotFoundException($"Role with Id {request.Id} not found");

        await _roleRepository.DeleteAsync(role);
        return true;
    }
}
