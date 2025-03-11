using Ambev.DeveloperEvaluation.Base.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Base.WebApi;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
