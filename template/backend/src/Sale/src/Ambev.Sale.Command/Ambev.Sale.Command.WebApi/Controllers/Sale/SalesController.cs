using Ambev.Base.WebApi;
using Ambev.Sale.Command.Application.Sale.Cancel;
using Ambev.Sale.Command.Application.Sale.Create;
using Ambev.Sale.Command.Application.Sale.Delete;
using Ambev.Sale.Command.Application.Sale.Update;
using Ambev.Sale.Core.Domain.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Sale.Command.WebApi;

/// <summary>
/// Sale EndPoint
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ISaleQueryRepository _saleQueryRepository;

    public SalesController(IMediator mediator, IMapper mapper, ISaleQueryRepository saleQueryRepository)
    {
        _saleQueryRepository = saleQueryRepository; 
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// creates the sale and generates the item discounts and calculates the total sale
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResult>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResult>(response)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseWithData<CreateSaleResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }


    /// <summary>
    /// Responsible for changing only the sales data
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ModifySale([FromBody] UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new UpdateSaleCommandValidator(_saleQueryRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<UpdateSaleResult>
            {
                Success = true,
                Message = "Sale modified successfully",
                Data = _mapper.Map<UpdateSaleResult>(response)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseWithData<UpdateSaleResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Responsible for physically deleting the sale
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteSale([FromBody] DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new DeleteSaleCommandValidator(_saleQueryRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<DeleteSaleResult>
            {
                Success = true,
                Message = "Sale deleted successfully",
                Data = _mapper.Map<DeleteSaleResult>(response)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseWithData<DeleteSaleResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Responsible for cancel the sale, chance your status
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("Cancel")]
    public async Task<IActionResult> CancelSale([FromBody] CancelSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CancelSaleCommandValidator(_saleQueryRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var response = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CancelSaleResult>
            {
                Success = true,
                Message = "Sale cancelled successfully",
                Data = _mapper.Map<CancelSaleResult>(response)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseWithData<CancelSaleResult>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}

