using System;
using Infrastructure.Schedule.Models;

namespace Infrastructure.Schedule.Options
{
    public class JobOptions
    {
        public string Name { get; set; }

        public Type ExecutorType { get; set; }

        public JobInfo JobInfo { get; set; }
    }
}