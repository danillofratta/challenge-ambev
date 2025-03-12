using Ambev.Base.WebApi;
using Ambev.Sale.Query.Application.Sale.GetById;
using Ambev.Sale.Query.Application.Sale.GetItensOfSale;
using Ambev.Sale.Query.Application.Sale.GetListPaginated;
using Ambev.Sale.Query.Domain.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Sale.WebApi.Controllers.Sale;

/// <summary>
/// Sale EndPoint
/// TODO: create versioning 
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SalesQueryController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISaleQueryRepository _repository;

    public SalesQueryController(IMediator mediator, IMapper mapper, ISaleQueryRepository repository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginatedProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetSaleListPaginatedQuery(pageNumber, pageSize);
            var paginatedResult = await _mediator.Send(query);

            return OkPaginated(paginatedResult);
        }
        catch (Exception ex)
        {
            return NotFound(new ApiResponseWithData<GetSaleListPaginatedQuery>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Responsible to return sale and yours itens
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetSaleByIdQuery { Id = id };
            var validator = new GetSaleByIdQueryValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetSaleByIdQueryResult>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<GetSaleByIdQueryResult>(response)
            });
        }
        catch (Exception ex)
        {
            return NotFound(new ApiResponseWithData<GetSaleByIdQueryResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Return ONLY itens of Sale
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetItensOfSale/{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetItensOfSaleQueryResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItensOFSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetItensOfSaleQuery { Id = id };
            var validator = new GetItensOfSaleQueryValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<List<GetItensOfSaleQueryResult>>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<List<GetItensOfSaleQueryResult>>(response)
            });
        }
        catch (Exception ex)
        {
            return NotFound(new ApiResponseWithData<List<GetItensOfSaleQueryResult>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

}

