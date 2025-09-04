using FluentValidation;
using ProjectManagement.Application.DTOs.Roles;

namespace ProjectManagement.Application.Validators.Roles;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}