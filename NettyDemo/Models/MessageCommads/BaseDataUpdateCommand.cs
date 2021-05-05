using System;
using System.Collections.Generic;

namespace NettyDemo.Models.MessageCommads
{
    public class BaseDataUpdateCommand
    {
        public List<BaseDataUpdateContent> UpdateContents { get; set; }

        public DateTimeOffset TriggerTime { get; set; }
    }

    public class BaseDataUpdateContent
    {
        public string DataType { get; set; }

        public string DataKey { get; set; }
    }
}