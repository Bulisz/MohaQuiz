using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Hubs;

public class GameControlHub : Hub, IGameControlHub
{
    public async Task SendTeamNamesToAdmin(IQuizService quizService)
    {
        IEnumerable<string> teamNames = await quizService.GetAllTeamNamesAsync();
        await Clients.All.SendAsync("GetTeamNames", teamNames);
    }

    public async Task SendGameProcessState(GameProcessStateDTO actualGameProcess)
    {
        await Clients.All.SendAsync("GetGameProcessState", actualGameProcess);
    }
}
