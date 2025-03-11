using Ambev.Base.WebApi;
using Ambev.Sale.Query.Domain.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.Sale.Query.Application.SaleItem.GetById;

namespace Ambev.Sale.WebApi.Controllers.SaleItem;

/// <summary>
/// Saleitem EndPoint Query
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SaleItensQueryController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISaleItemQueryRepository _repository;

    public SaleItensQueryController(IMediator mediator, IMapper mapper, ISaleItemQueryRepository repository)
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
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleItemByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleItemById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetSaleItemByIdQuery { Id = id };
            var validator = new GetSaleItemByIdQueryValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<GetSaleItemByIdQueryResult>
            {
                Success = true,
                Message = "Sale Item retrieved successfully",
                Data = _mapper.Map<GetSaleItemByIdQueryResult>(response)
            });
        }
        catch (Exception ex)
        {
            return NotFound(new ApiResponseWithData<GetSaleItemByIdQueryResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

}

