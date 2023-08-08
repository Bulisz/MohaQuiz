namespace MohaQuiz.Backend.Models.DTOs;

public record RoundRecordDTO
{
    public string GameName { get; set; } = string.Empty;
    public string RoundTypeName { get; set; } = string.Empty;
    public string RoundName { get; set; } = string.Empty;
    public List<QuestionRecordDTO> Questions { get; set; } = null!;
}

public record QuestionRecordDTO
{
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int FullScore { get; set; }
    public List<CorrectAnswerRecordDTO> CorrectAnswers { get; set; } = null!;
}

public record CorrectAnswerRecordDTO
{
    public string CorrectAnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}