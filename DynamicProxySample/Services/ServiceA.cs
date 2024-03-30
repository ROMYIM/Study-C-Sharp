using DynamicProxySample.Interfaces;
using DynamicProxySample.Repositories;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Services;

public class ServiceA : IServiceA
{
    private readonly ILogger<ServiceA> _logger;

    // private readonly TestRepository _testRepository;

    public ServiceA(ILogger<ServiceA> logger)
    {
        _logger = logger;
        // _testRepository = testRepository;
    }

    public int Test(ref int number)
    {
        _logger.LogInformation("original instance log {Number}", number++);
        // Console.WriteLine($"execute service. number is {number++}");
        return number;
    }

    public Task<string> TestDbAsync(int id, string name)
    {
        _logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        
        // var test = await _testRepository.GetAsync(id);
        
        _logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        
        // if (test is null) return "no result";
        //
        // test.Name = name;
        // await _testRepository.UpdateAsync(test);
        
        _logger.LogInformation("thread id {}", Environment.CurrentManagedThreadId);
        return Task.FromResult(name);
    }
}