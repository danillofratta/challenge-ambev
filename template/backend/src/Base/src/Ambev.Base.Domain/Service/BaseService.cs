
using Ambev.Base.Domain.Validation;

namespace Ambev.Base.Domain.Services;
/// <summary>
/// TODO create default method to execute the process
/// </summary>
public class BaseService
{
    public List<ValidationErrorDetail> _ListError { get; set; } = new();
}

