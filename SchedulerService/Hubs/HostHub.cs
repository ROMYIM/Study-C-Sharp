using Microsoft.AspNetCore.SignalR;

namespace SchedulerService.Hubs
{
    public class HostHub : Hub
    {
        public async Task StartApplicationAsync()
        {

        }

        public async Task StopApplicationAsync()
        {

        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
