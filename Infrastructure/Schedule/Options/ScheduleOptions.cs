using System.Collections.Generic;

namespace Infrastructure.Schedule.Options
{
    public class ScheduleOptions
    {
        public SignalRClientOptions SignalRClientOptions { get; set; } = new SignalRClientOptions();

        public IDictionary<string, JobOptions> JobOptionsMap { get; } = new Dictionary<string, JobOptions>();
    }
}