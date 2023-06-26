namespace MohaQuiz.Backend.Models.DTOs;

public record TeamScoreSummaryDTO
{
    public List<double> TeamScoresPerRound { get; set; } = null!;
}
