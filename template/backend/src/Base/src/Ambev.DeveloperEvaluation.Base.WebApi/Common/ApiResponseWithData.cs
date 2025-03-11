namespace Ambev.DeveloperEvaluation.Base.WebApi;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
