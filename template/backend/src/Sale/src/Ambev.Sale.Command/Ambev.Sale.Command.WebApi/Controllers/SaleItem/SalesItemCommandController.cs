using Ambev.Base.WebApi;
using Ambev.Sale.Command.Application.SaleItem.Cancel;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Sale.Command.WebApiItem;

/// <summary>
/// Sale EndPoint
/// TODO: create versioning 
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SalesItemCommandController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesItemCommandController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Responsible for canceling the item from sale, also recalculating the discount on the items and the total sale.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("Cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleItemResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelItemSale([FromBody] CancelSaleItemCommand request, CancellationToken cancellationToken)
    {                
        var response = await _mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CancelSaleItemResult>
        {
            Success = true,
            Message = "Item cancelled successfully",
            Data = _mapper.Map<CancelSaleItemResult>(response)
        });
    }
}

