using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class JobInfo
    {
        [Required]
        public string JobKey { get; set; }

        [Required]
        public string CronExpression { get; set; }

        public string Description { get; set; }
    }
}