using Ambev.DeveloperEvaluation.Base.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Base.Domain.Service
{
    /// <summary>
    /// TODO create default method to execute the process
    /// </summary>
    public class BaseService
    {
        public List<ValidationErrorDetail> _ListError { get; set; } = new();
    }
}
