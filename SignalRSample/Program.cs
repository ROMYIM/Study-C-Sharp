using Infrastructure.Schedule.Extensions;
using SignalRSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("jobinfo.json", true, true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSchedule(options =>
{
    options.Host = "http://localhost:5020";
    options.HandShakeTimeout = TimeSpan.FromSeconds(10);
    options.KeepAliveInterval = TimeSpan.FromMinutes(1);
    options.ServerTimeout = TimeSpan.FromSeconds(10);
}).AddScheduleJob<TestJobService>("JobInfo", builder.Configuration.GetSection("JobInfo"));

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