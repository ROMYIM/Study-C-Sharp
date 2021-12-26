namespace Infrastructure.Models
{
    public class FilesDeleteJobOptions
    {
        public string JobKey { get; set; }
        
        public string DirectoryPath { get; set; }

        public string CronExpression { get; set; }

        public string Description { get; set; }
    }
}