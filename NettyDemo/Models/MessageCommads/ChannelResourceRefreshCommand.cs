using System;
using System.Collections.Generic;

namespace NettyDemo.Models.MessageCommads
{
    public class ChannelResourceRefreshCommand
    {
        public string RefreshTypeKey { get; set; }

        public List<string> RefreshIds { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset TriggerTime { get; set; }
    }
}