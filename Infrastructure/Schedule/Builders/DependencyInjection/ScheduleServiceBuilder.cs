using Infrastructure.Schedule.Models;
using Infrastructure.Schedule.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Builders.DependencyInjection
{
    public class ScheduleServiceBuilder
    {
        public IServiceCollection Services { get; }

        public OptionsBuilder<ScheduleOptions> ScheduleOptionsBuilder { get; }

        public ScheduleServiceBuilder(IServiceCollection services, OptionsBuilder<ScheduleOptions> scheduleOptionsBuilder)
        {
            Services = services;
            ScheduleOptionsBuilder = scheduleOptionsBuilder;
        }
    }
}