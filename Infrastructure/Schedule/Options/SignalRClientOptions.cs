using System;

namespace Infrastructure.Schedule.Options
{
    public class SignalRClientOptions
    {
        public string Host { get; set; }

        public TimeSpan HandShakeTimeout { get; set; }

        public TimeSpan KeepAliveInterval { get; set; }

        public TimeSpan ServerTimeout { get; set; }
    }
}