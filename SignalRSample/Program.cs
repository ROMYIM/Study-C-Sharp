using Infrastructure.Schedule.Extensions;
using SignalRSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("jobinfo.json", true, true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScheduleJob<TestJobService>(builder.Configuration.GetSection("JobInfo"));
builder.Services.AddScheduleJob<TestJob2Service>(info =>
{
    info.Description = "测试任务2";
    info.Host = "http://localhost:5020/signalr";
    info.MethodName = nameof(TestJob2Service);
    info.JobKey = nameof(TestJob2Service);
    info.CronExpression = "0/5 * * * * ? ";
    info.KeepAliveInterval = TimeSpan.FromSeconds(4);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();