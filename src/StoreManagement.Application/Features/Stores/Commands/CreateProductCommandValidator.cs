using FluentValidation;

namespace Application.Features.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Ürün adı zorunludur.")
            .MaximumLength(150).WithMessage("Ürün adı en fazla 150 karakter olabilir.");

        RuleFor(p => p.SKU)
            .NotEmpty().WithMessage("Ürün stok kodu (SKU) zorunludur.")
            .MaximumLength(50).WithMessage("SKU en fazla 50 karakter olabilir.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");

        RuleFor(p => p.StoreId)
            .NotEmpty().WithMessage("Ürünün ait olduğu mağaza kimliği (StoreId) belirtilmelidir.");
    }
}