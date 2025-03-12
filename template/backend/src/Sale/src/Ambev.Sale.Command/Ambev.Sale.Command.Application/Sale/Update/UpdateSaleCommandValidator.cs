using Ambev.Sale.Core.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Command.Application.Sale.Update
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        private readonly ISaleQueryRepository _repository;

        public UpdateSaleCommandValidator(ISaleQueryRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale is required")
                .MustAsync(SaleExists).WithMessage("Sale not found");

            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();                                 
        }

        private async Task<bool> SaleExists(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
