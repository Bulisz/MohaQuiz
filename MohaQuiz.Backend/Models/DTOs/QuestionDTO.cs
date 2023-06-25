namespace MohaQuiz.Backend.Models.DTOs;

public record QuestionDTO
{
    public int QuestionNumber { get; set; }
    public int FullScore { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<CorrectAnswerDTO> CorrectAnswers { get; set; }
}
