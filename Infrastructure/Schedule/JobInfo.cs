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

        public virtual JobInfo New(JobInfo jobInfo)
        {
            if (jobInfo == null) throw new ArgumentNullException(nameof(jobInfo));
            
            JobKey = jobInfo.JobKey;
            CronExpression = jobInfo.CronExpression;
            MethodName = jobInfo.MethodName;
            Description = jobInfo.Description;

            return this;
        }
    }

    public class JobInfo<T> : JobInfo where T : IJobExecutor
    {
        [Required]
        public override string JobKey { get; set; }

        [Required]
        public override string CronExpression { get; set; }
        
        public override string Description { get; set; }
        
        public override string MethodName => typeof(T).FullName;
    }
}