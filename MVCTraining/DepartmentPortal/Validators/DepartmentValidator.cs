using DepartmentPortal.DTOs;
using DepartmentPortal.Models.Entities;
using FluentValidation;

namespace DepartmentPortal.Validators;

public class DepartmentValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentValidator()
    {
        ConfigureNameValidation();
        ConfigureLocationValidation();

    }
    private void ConfigureNameValidation()
    {
        RuleFor(x => x.DepartmentName)
            .NotEmpty()
            .WithMessage("Department name is required.")
            .Length(2, 50)
            .WithMessage("Name must be between 2 and 50 characters.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Name can only contain letters and spaces.");
    }
    private void ConfigureLocationValidation()
    {
        RuleFor(x => x.Location)
            .NotEmpty()
            .WithMessage("Location is required.")
            .Length(2, 50)
            .WithMessage("Name must be between 2 and 50 characters.")
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Name can only contain letters and spaces.");
    }
}