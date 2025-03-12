using Ambev.Sale.Query.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Query.Application.Sale.GetItensOfSale
{
    public class GetItensOfSaleQueryValidator : AbstractValidator<GetItensOfSaleQuery>
    {
        private readonly ISaleQueryRepository _repository;
        public GetItensOfSaleQueryValidator(ISaleQueryRepository repository)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale is required")
                .MustAsync(SaleExists).WithMessage("Sale not found");
            _repository = repository;
        }

        private async Task<bool> SaleExists(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
