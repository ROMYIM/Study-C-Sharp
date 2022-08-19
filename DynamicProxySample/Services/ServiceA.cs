using DynamicProxySample.Interfaces;
using DynamicProxySample.Repositories;
using Microsoft.Extensions.Logging;

namespace DynamicProxySample.Services;

public class ServiceA : IServiceA
{
    private readonly ILogger<ServiceA> _logger;

    private readonly TestRepository _testRepository;

    public ServiceA(ILogger<ServiceA> logger, TestRepository testRepository)
    {
        _logger = logger;
        _testRepository = testRepository;
    }

    public int Test(ref int number)
    {
        _logger.LogInformation("original instance log {Number}", number++);
        // Console.WriteLine($"execute service. number is {number++}");
        return number;
    }

    public async ValueTask<string> TestDbAsync(int id, string name)
    {
        var test = await _testRepository.GetAsync(id);
        if (test is null) return "no result";
        test.Name = name;
        await _testRepository.UpdateAsync(test);
        return test.Name;
    }
}