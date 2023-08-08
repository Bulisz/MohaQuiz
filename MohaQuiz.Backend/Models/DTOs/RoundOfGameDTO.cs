namespace MohaQuiz.Backend.Models.DTOs;

public record RoundOfGameDTO
{
    public string GameName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
}
