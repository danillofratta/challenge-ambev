using Ambev.Sale.Query.Domain.Repository;
using FluentValidation;

namespace Ambev.Sale.Query.Domain.Validation;

/// <summary>
/// Validates if the sale exists
/// </summary>
public class SaleExistsValidator : AbstractValidator<Ambev.Sale.Query.Domain.Entities.Sale>
{
    private readonly ISaleQueryRepository _repository;

    public SaleExistsValidator(ISaleQueryRepository repository)
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