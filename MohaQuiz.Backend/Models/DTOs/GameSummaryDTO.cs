namespace MohaQuiz.Backend.Models.DTOs;

public record GameSummaryDTO
{
    public string TeamName { get; set; } = string.Empty;
    public double TeamScore { get; set; }
}