using FluentValidation;

namespace Application.Features.Stores.Commands;

public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Mağaza adı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Mağaza adı en fazla 100 karakter olabilir.");

        RuleFor(s => s.Address)
            .NotEmpty().WithMessage("Mağaza adresi boş bırakılamaz.")
            .MaximumLength(250).WithMessage("Mağaza adresi en fazla 250 karakter olabilir.");
    }
}