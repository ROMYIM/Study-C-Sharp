using Infrastructure.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Quartz;

namespace SchedulerService.Jobs;

public class FilesDeleteJob : IJob
{
    private readonly ILogger<FilesDeleteJob> _logger;

    private readonly IOptionsMonitor<FilesDeleteJobOptions> _optionsMonitor;

    public FilesDeleteJob(ILogger<FilesDeleteJob> logger, IOptionsMonitor<FilesDeleteJobOptions> optionsMonitor)
    {
        _logger = logger;
        _optionsMonitor = optionsMonitor;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var options = _optionsMonitor.CurrentValue;
        var fileProvider = new PhysicalFileProvider(options.DirectoryPath);
        _logger.LogInformation("当前时间：{}", DateTimeOffset.Now);
        _logger.LogInformation("当前目录：{}", fileProvider.Root);
        return Task.CompletedTask;
    }
}