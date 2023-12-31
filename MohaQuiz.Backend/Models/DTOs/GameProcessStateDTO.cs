﻿namespace MohaQuiz.Backend.Models.DTOs;

public record GameProcessStateDTO
{
    public string GameName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
    public int QuestionNumber { get; set; }
    public bool IsGameStarted { get; set; }
    public bool IsScoring { get; set; }
    public bool IsGameFinished { get; set; }
}
