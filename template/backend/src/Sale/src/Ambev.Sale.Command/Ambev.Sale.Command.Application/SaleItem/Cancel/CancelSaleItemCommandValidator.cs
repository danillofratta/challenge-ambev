using Ambev.Sale.Core.Domain.Repository;
using FluentValidation;


namespace Ambev.Sale.Command.Application.SaleItem.Cancel
{
    public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
    {
        private readonly ISaleItemQueryRepository _repository;

        public CancelSaleItemCommandValidator(ISaleItemQueryRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Item is required")
                .MustAsync(ExistsSaleItem).WithMessage("Item not found");
        }

        private async Task<bool> ExistsSaleItem(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
