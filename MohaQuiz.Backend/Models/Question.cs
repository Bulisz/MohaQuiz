﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("Questions")]
public class Question
{
    public int Id { get; set; }
    public int QuestionNumber { get; set; }
    public int FullScore { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public Round Round { get; set; } = null!;
    public ICollection<CorrectAnswer> CorrectAnswers { get; set; } = null!;
    public ICollection<TeamAnswer> TeamAnswers { get; set; } = null!;
}
