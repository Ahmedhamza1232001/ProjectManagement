using Microsoft.AspNetCore.SignalR;

namespace ProjectManagement.Api.SignalR;

public class TaskHub : Hub
{
    public async Task SendTaskUpdate(string taskId, string message)
    {
        await Clients.All.SendAsync("ReceiveTaskUpdate", taskId, message);
    }
}
