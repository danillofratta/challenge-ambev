using Ambev.Base.WebApi;
using Ambev.Sale.Query.Domain.Repository;
using AutoMapper;
using MediatR;


namespace Ambev.Sale.Query.Application.Sale.GetListPaginated
{
    public class GetSaleListPaginatedQueryHandler : IRequestHandler<GetSaleListPaginatedQuery, PaginatedList<GetSaleListPaginatedQueryResult>>
    {
        private readonly ISaleQueryRepository _saleQueryRepository;
        private readonly IMapper _mapper;

        public GetSaleListPaginatedQueryHandler(ISaleQueryRepository saleQueryRepository, IMapper mapper)
        {
            _saleQueryRepository = saleQueryRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetSaleListPaginatedQueryResult>> Handle(GetSaleListPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = await _saleQueryRepository.GetAllAsync();

            var result = _mapper.ProjectTo<GetSaleListPaginatedQueryResult>(query);

            return await PaginatedList<GetSaleListPaginatedQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}
