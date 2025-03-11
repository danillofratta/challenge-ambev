using MediatR;
using FluentValidation;
using AutoMapper;
using Ambev.Sale.Query.Domain.Repository;

namespace Ambev.Sale.Query.Application.Sale.GetById;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdQueryResult>
{
    private readonly ISaleQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(ISaleQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetSaleByIdQueryResult> Handle(GetSaleByIdQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetSaleByIdQueryValidator(_repository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Sale = await _repository.GetByIdAsync(command.Id);
        if (Sale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        return _mapper.Map<GetSaleByIdQueryResult>(Sale);
    }
}

