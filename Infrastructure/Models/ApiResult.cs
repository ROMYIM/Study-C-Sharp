using System.Diagnostics;

namespace Infrastructure.Models;

public abstract class ApiResult
{
    public bool Success { get; set; }
    
    public virtual object Data { get; set; }
    
    public virtual string Message { get; set; }
}

public class TraceApiResult : ApiResult
{
    public virtual string TraceId { get; set; } = Activity.Current?.TraceId.ToString();
}