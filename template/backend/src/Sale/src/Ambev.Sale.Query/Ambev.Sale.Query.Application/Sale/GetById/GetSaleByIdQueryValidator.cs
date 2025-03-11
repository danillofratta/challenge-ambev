using Ambev.Sale.Query.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Query.Application.Sale.GetById
{
    public class GetSaleByIdQueryValidator : AbstractValidator<GetSaleByIdQuery>
    {
        private readonly ISaleQueryRepository _repository;
        public GetSaleByIdQueryValidator(ISaleQueryRepository repository)
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
