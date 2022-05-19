using System.ComponentModel;

namespace SchedulerService.Models;

public enum HostStatus : byte
{
    [Description("停止")]
    Stopped,

    [Description("运行中")]
    Executing,

}