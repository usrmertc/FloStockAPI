using FloAPI.Business.Models;

namespace FloAPI.Business.Responses;

public class ServiceRespond<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public ServiceRespond()
    {
        Errors = new List<string>();
    }
}