using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Query.Domain.Repository;

namespace Ambev.Sale.Query.Application.Sale.GetItensOfSale;

public class GetItensOfSaleQueryHandler : IRequestHandler<GetItensOfSaleQuery, List<GetItensOfSaleQueryResult>>
{
    private readonly ISaleQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetItensOfSaleQueryHandler(ISaleQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetItensOfSaleQueryResult>> Handle(GetItensOfSaleQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetItensOfSaleQueryValidator(_repository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Sale = await _repository.GetByIdAsync(command.Id);
        if (Sale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");        

        return _mapper.Map<List<GetItensOfSaleQueryResult>>(Sale.SaleItens);
    }
}

