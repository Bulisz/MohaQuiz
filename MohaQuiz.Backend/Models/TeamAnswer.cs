using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("TeamAnswers")]
public class TeamAnswer
{
    public int Id { get; set; }
    public string? TeamAnswerText { get; set; }
    public double? GivenScore { get; set; }
    public Question Question { get; set; }
    public Team Team { get; set; }
}
