using Infrastructure.Schedule.Extensions;
using Infrastructure.Schedule.Options;
using ScheduleApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSchedule(options =>
{
    options.SignalRClientOptions = new SignalRClientOptions()
    {
        Host = "http://localhost:5020/signalr",
        HandShakeTimeout = TimeSpan.FromSeconds(10),
        KeepAliveInterval = TimeSpan.FromMinutes(1),
        ServerTimeout = TimeSpan.FromSeconds(10)
    };
}).AddScheduleJob<TestJob2>("TestJob2", info =>
{
    info.Description = "第二个模拟测试的任务";
    info.CronExpression = "0/5 * * * * ? ";
    info.JobKey = "Demo2";
    info.MethodName = "Demo2Execute";
}).AddScheduleJob<StopHostJob>("StopHost", info =>
{
    info.Description = "模拟结束进程";
    info.CronExpression = "0/30 * * * * ? ";
    info.JobKey = "stop test";
    info.MethodName = "stop host";
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();