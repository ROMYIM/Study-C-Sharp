using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NettyDemo.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public string Host { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTimeOffset LatestSigninTime { get; set; }
    }

    public static class ClientType
    {
        public const string RpsClient = "RpsClient";

        public const string FlytWebApi = "FlytWebApi";
    }
}