using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("Teams")]
public class Team
{
    public int Id { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public ICollection<TeamAnswer> TeamAnswers { get; set; } = new List<TeamAnswer>();
}
