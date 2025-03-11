using Ambev.Base.WebApi;
using Ambev.Sale.Query.Application.Sale.GetById;
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

}

