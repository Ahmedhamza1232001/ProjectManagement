using Microsoft.AspNetCore.SignalR;

namespace ProjectManagement.Infrastructure.SignalR;

public class TaskHub : Hub
{
    public async Task SendTaskUpdate(string taskId, string status)
    {
        await Clients.All.SendAsync("ReceiveTaskUpdate", taskId, status);
    }
}
