using System;

namespace Infrastructure.Schedule.Models
{
    public class JobExecuteResult
    {
        public string JobId { get; set; }

        public DateTimeOffset ExecuteTime { get; set; }

        public string JobKey { get; set; }

        public string Message { get; set; }

        public JobResult Result { get; set; }

        public bool Success => Result == JobResult.Success;
    }

    public enum JobResult
    {
        Executing,
        Success,
        Failed,
        
    }
}