using Ambev.Sale.Core.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Command.Application.Sale.Delete
{
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        private readonly ISaleQueryRepository _repository;
        public DeleteSaleCommandValidator(ISaleQueryRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale is required")
                .MustAsync(ExistInDatabase).WithMessage("Sale not found");
        }
        private async Task<bool> ExistInDatabase(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
