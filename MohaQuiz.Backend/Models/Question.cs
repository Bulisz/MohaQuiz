using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("Questions")]
public class Question
{
    public int Id { get; set; }
    public int RoundNumber { get; set; }
    public int QuestionNumber { get; set; }
    public int FullScore { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public Round Round { get; set; }
    public ICollection<CorrectAnswer> CorrectAnswers { get; set; }
    public ICollection<TeamAnswer> TeamAnswers { get; set; }
}
