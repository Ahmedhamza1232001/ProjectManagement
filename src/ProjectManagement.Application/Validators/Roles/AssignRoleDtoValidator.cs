using FluentValidation;
using ProjectManagement.Application.DTOs.Roles;

namespace ProjectManagement.Application.Validators.Roles;

public class AssignRoleDtoValidator : AbstractValidator<AssignRoleDto>
{
    public AssignRoleDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
}