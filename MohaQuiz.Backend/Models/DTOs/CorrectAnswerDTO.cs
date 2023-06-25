namespace MohaQuiz.Backend.Models.DTOs;

public record CorrectAnswerDTO
{
    public string CorrectAnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}