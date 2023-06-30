namespace MohaQuiz.Backend.Models.DTOs;

public record NullableMessageDTO
{
    public string TeamName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
