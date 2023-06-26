namespace MohaQuiz.Backend.Models.DTOs;

public record TeamAnswerDTO
{
    public string TeamName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
    public int QuestionNumber { get; set; }
    public string TeamAnswerText { get; set; } = string.Empty;
}
