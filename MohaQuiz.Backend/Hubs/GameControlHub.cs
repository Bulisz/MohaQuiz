using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Models.DTOs;
using MohaQuiz.Backend.Services;

namespace MohaQuiz.Backend.Hubs;

public class GameControlHub : Hub
{
    public async Task SendRoundDetailsAsync(IQuizService quizService)
    {
        int actualRoundNumber = GameProcessService.GetActualGameProcess().RoundNumber;
        RoundDetailsDTO roundDetailsDTO = await quizService.GetRoundDetailsAsync(actualRoundNumber);
        await Clients.All.SendAsync("GetRoundDetails", roundDetailsDTO);
    }
}
