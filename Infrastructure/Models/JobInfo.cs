using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class JobInfo
    {
        [Required]
        public string JobKey { get; set; }

        [Required]
        public string CronExpression { get; set; }

        [Required]
        public string Host { get; set; }

        public string Description { get; set; }

        public RpcType Rpc { get; set; }

        public TimeSpan? KeepAliveInterval { get; set; }
    }
    
    public enum RpcType : byte
    {
        SignalR,
        WebApi,
        Grpc
    }
}