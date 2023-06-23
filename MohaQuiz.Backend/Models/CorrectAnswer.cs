using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("CorrectAnswers")]
public class CorrectAnswer
{
    public int Id { get; set; }
    public string CorrectAnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Question Question { get; set; }
}
