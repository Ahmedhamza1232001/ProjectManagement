using FluentValidation;
using ProjectManagement.Application.DTOs.Projects;

namespace ProjectManagement.Application.Validators.Projects;

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required")
            .MaximumLength(200).WithMessage("Project name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Project description is required")
            .MaximumLength(1000).WithMessage("Project description cannot exceed 1000 characters");
    }
}
