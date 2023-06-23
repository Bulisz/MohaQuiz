using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;

namespace MohaQuiz.Backend.Hubs;

public class GameControlHub : Hub
{
    public async Task SendMessage(IGameProcessService gameProcessService)
    {
        await Clients.All.SendAsync("MessageListener", "Message");
    }
}
