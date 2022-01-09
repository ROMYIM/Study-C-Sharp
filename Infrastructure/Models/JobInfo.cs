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

        [Required]
        public string MethodName { get; set; }

        public string Description { get; set; }

        public uint ConcurrentCount { get; set; }

        [Obsolete("暂时只支持SignalR。后面考虑统合其他通信方式")]
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