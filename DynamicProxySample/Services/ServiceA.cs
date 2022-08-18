using DynamicProxySample.Interfaces;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Services;

public class ServiceA : IServiceA
{
    private readonly ILogger<ServiceA> _logger;

    public ServiceA(ILogger<ServiceA> logger)
    {
        _logger = logger;
    }

    public int Test(int number)
    {
        _logger.LogInformation("original instance log {Number}", number++);
        return number;
    }
}