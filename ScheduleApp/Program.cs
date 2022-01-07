using Infrastructure.Schedule.Extensions;
using ScheduleApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScheduleJob<TestJob2>(info =>
{
    info.Description = "第二个模拟测试的任务";
    info.Host = "http://localhost:5020/signalr";
    info.CronExpression = "0/10 * * * * ? ";
    info.JobKey = "Demo2";
    info.MethodName = "Demo2Execute";
    info.KeepAliveInterval = TimeSpan.FromMinutes(1);
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();