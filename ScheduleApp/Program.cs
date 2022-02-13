using Infrastructure.Schedule.Extensions;
using ScheduleApp;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSchedule(options =>
{
    options.Host = "http://localhost:5020";
    options.HandShakeTimeout = TimeSpan.FromSeconds(10);
    options.KeepAliveInterval = TimeSpan.FromMinutes(1);
    options.ServerTimeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddSchedule(options =>
{
    options.Host = "http://localhost:5020";
    options.HandShakeTimeout = TimeSpan.FromSeconds(10);
    options.KeepAliveInterval = TimeSpan.FromMinutes(1);
    options.ServerTimeout = TimeSpan.FromSeconds(10);
}).AddScheduleJob<TestJob2>("TestJob2", info =>
{
    info.Description = "第二个模拟测试的任务";
    info.CronExpression = "*/5 * * * * ? ";
    info.JobKey = "Demo2";
    
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();