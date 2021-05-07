using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NettyDemo.Infrastructure.Options
{
    public class RabbitMqOptions
    {
        public List<string> Hosts { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string HeartBeat { get; set; }

        public bool AutomaticRecoveryEnabled { get; set; }

        public string NetworkRecoveryInterval { get; set; }

        public string VirtualHost { get; set; }
    }
}
