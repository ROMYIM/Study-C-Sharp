using System.Collections.Generic;

namespace Infrastructure.Schedule.Options
{
    public class ScheduleOptions
    {
        public SignalRClientOptions SignalRClientOptions { get; set; } = new SignalRClientOptions();
        
        public IList<JobOptions> JobOptionsList { get; } = new List<JobOptions>();
    }
}