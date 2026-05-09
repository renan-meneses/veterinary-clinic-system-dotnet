using FluentValidation;
using VeterinaryClinic.Application.Commands.Animals;

public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Animal name is required.")
            .MaximumLength(100).WithMessage("Animal name must not exceed 100 characters.");

        RuleFor(x => x.Species)
            .NotEmpty().WithMessage("Species is required.");

        RuleFor(x => x.Sex)
            .NotEmpty().WithMessage("Sex is required.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).When(x => x.Weight.HasValue)
            .WithMessage("Weight must be greater than zero.");

        RuleFor(x => x.TutorIds)
            .NotEmpty().WithMessage("At least one tutor must be assigned.");
    }
}

public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Animal ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Animal name is required.")
            .MaximumLength(100).WithMessage("Animal name must not exceed 100 characters.");

        RuleFor(x => x.Species)
            .NotEmpty().WithMessage("Species is required.");

        RuleFor(x => x.Sex)
            .NotEmpty().WithMessage("Sex is required.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).When(x => x.Weight.HasValue)
            .WithMessage("Weight must be greater than zero.");
    }
}

public class AddTutorToAnimalCommandValidator : AbstractValidator<AddTutorToAnimalCommand>
{
    public AddTutorToAnimalCommandValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("Animal ID is required.");

        RuleFor(x => x.TutorId)
            .NotEmpty().WithMessage("Tutor ID is required.");
    }
}