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

    public async Task ScoringReadyAsync(string teamName)
    {
        await Clients.All.SendAsync("GetReadyTeamName", teamName);
    }

    public async Task SendExtraAnswer(TeamAnswerDTO answer)
    {
        await Clients.All.SendAsync("GetExtraAnswer", answer);
    }

    public async Task ConfirmExtraAnswer(string teamName)
    {
        await Clients.All.SendAsync("GetConfirmationOfExtraAnswer", teamName);
    }

    public async Task NullableAnswers(RoundAnswersOfTeamDTO answers)
    {
        await Clients.All.SendAsync("GetNullableAnswers", answers);
    }

    public async Task SendNullableMessage(NullableMessageDTO message)
    {
        await Clients.All.SendAsync("GetNullableMessage", message);
    }
}
