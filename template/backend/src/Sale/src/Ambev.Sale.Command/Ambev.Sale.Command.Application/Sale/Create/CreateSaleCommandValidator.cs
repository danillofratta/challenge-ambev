using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.Sale.Command.Domain.Validation;
using FluentValidation;
using FluentValidation.Validators;


namespace Ambev.Sale.Command.Application.Sale.Create
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(c => c.BranchId)
                .NotEmpty().WithMessage("Branch ID is required.");
            RuleFor(c => c.BranchName)
                .NotEmpty().WithMessage("Branch name is required.");

            RuleFor(c => c.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(c => c.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(c => c.SaleItens)
                .NotEmpty().WithMessage("The sale must contain at least one item.");
            RuleForEach(c => c.SaleItens).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("Product ID is required for each sale item.");

                items.RuleFor(i => i.ProductName)
                    .NotEmpty().WithMessage("Product Name is required for each sale item.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero for each sale item.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Unit price must be greater than zero for each sale item.");
            });
        }
    }
}
