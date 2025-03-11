using Ambev.Sale.Query.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Query.Application.SaleItem.GetById
{
    public class GetSaleItemByIdQueryValidator : AbstractValidator<GetSaleItemByIdQuery>
    {
        private readonly ISaleItemQueryRepository _repository;
        public GetSaleItemByIdQueryValidator(ISaleItemQueryRepository repository)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale Item is required")
                .MustAsync(SaleExists).WithMessage("Sale Item not found");
            _repository = repository;
        }

        private async Task<bool> SaleExists(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
