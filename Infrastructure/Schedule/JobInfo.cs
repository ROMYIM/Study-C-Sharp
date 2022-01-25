using System;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;
using Infrastructure.Schedule.JobExecutors;

namespace Infrastructure.Schedule
{
    public class JobInfo
    {
        [Required]
        public virtual string JobKey { get; set; }

        [Required]
        public virtual string CronExpression { get; set; }

        public virtual string MethodName { get; set; }

        public virtual string Description { get; set; }
    }

    public class JobInfo<T> : JobInfo where T : IJobExecutor
    {
        public JobInfo(JobInfo jobInfo)
        {
            base.JobKey = jobInfo.JobKey;
            base.CronExpression = jobInfo.CronExpression;
            base.Description = jobInfo.Description;
        }
        
        public override string MethodName => typeof(T).FullName;
    }
}