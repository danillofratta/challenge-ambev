using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Query.Domain.Repository;

namespace Ambev.Sale.Query.Application.SaleItem.GetById;

public class GetSaleItemByIdQueryHandler : IRequestHandler<GetSaleItemByIdQuery, GetSaleItemByIdQueryResult>
{
    private readonly ISaleItemQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetSaleItemByIdQueryHandler(ISaleItemQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetSaleItemByIdQueryResult> Handle(GetSaleItemByIdQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetSaleItemByIdQueryValidator(_repository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Sale = await _repository.GetByIdAsync(command.Id);
        if (Sale == null)
            throw new KeyNotFoundException($"Sale Item with ID {command.Id} not found");

        return _mapper.Map<GetSaleItemByIdQueryResult>(Sale);
    }
}

