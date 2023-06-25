namespace MohaQuiz.Backend.Models.DTOs;

public record GameProcessStateDTO
{
    public int RoundNumber { get; set; }
    public int QuestionNumber { get; set; }
    public bool IsGameStarted { get; set; }
}
