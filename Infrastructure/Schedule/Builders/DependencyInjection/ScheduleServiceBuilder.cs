using Infrastructure.Schedule.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Schedule.Builders.DependencyInjection
{
    public class ScheduleServiceBuilder
    {
        public IServiceCollection Services { get; }

        public OptionsBuilder<ScheduleOptions> ScheduleOptionsBuilder { get; }
        
        public OptionsBuilder<JobInfo> JobInfoOptionsBuilder { get; }

        public ScheduleServiceBuilder(IServiceCollection services, OptionsBuilder<ScheduleOptions> scheduleOptionsBuilder, OptionsBuilder<JobInfo> jobInfoOptionsBuilder)
        {
            Services = services;
            ScheduleOptionsBuilder = scheduleOptionsBuilder;
            JobInfoOptionsBuilder = jobInfoOptionsBuilder;
        }
    }
}