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
        string actualGameName = GameProcessService.GetActualGameProcess().GameName;
        RoundDetailsDTO roundDetailsDTO = await quizService.GetRoundDetailsAsync(new() { RoundNumber = actualRoundNumber, GameName = actualGameName });
        await Clients.All.SendAsync("GetRoundDetails", roundDetailsDTO);
    }

    public async Task ScoringReadyAsync(string teamName)
    {
        await Clients.All.SendAsync("GetReadyTeamName", teamName);
        await Clients.All.SendAsync("GetSummaryOfTeam");
    }

    public async Task FinishedScore()
    {
        await Clients.All.SendAsync("GetSummaryOfTeam");
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
