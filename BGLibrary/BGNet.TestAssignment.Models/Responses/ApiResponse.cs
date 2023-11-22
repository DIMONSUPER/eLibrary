namespace BGNet.TestAssignment.Models.Responses;

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}

