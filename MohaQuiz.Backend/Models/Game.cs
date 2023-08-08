using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("Games")]
public class Game
{
    public int Id { get; set; }
    public string GameName { get; set; } = string.Empty;
    public ICollection<Round> Rounds { get; set; } = null!;
}
