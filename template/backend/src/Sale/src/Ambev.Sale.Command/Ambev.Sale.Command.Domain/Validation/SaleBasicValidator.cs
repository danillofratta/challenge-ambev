using FluentValidation;

namespace Ambev.Sale.Command.Domain.Validation;

/// <summary>
/// Performs basic entity validations
/// </summary>
public class SaleBasicValidator : AbstractValidator<Ambev.Sale.Command.Domain.Entities.Sale>
{
    public SaleBasicValidator()
    {
        RuleFor(s => s.BranchId).NotEmpty();
        RuleFor(s => s.BranchName).NotEmpty();
        RuleFor(s => s.CustomerId).NotEmpty();
        RuleFor(s => s.CustomerName).NotEmpty();
        RuleFor(s => s.SaleItens).NotEmpty();

        RuleFor(s => s.TotalAmount)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.SaleItens)
            .NotEmpty().WithMessage("The sale must contain at least one item.");

        RuleForEach(x => x.SaleItens).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("Product ID is required");

            items.RuleFor(i => i.ProductName)
                .NotEmpty().WithMessage("Product Name is required");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than zero.");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        });
    }
}
