﻿namespace Ambev.DeveloperEvaluation.Base.WebApi;

public class PaginatedResponse<T> : ApiResponseWithData<IEnumerable<T>>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}