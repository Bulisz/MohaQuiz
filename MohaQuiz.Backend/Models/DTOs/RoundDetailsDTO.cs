﻿namespace MohaQuiz.Backend.Models.DTOs;

public record RoundDetailsDTO
{
    public int RoundNumber { get; set; }
    public string RoundName { get; set; } = string.Empty;
    public RoundTypeDTO RoundType { get; set; } = null!;
    public List<QuestionDTO> Questions { get; set; } = null!;
}
