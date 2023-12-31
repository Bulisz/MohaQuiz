﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("Rounds")]
public class Round
{
    public int Id { get; set; }
    public int RoundNumber { get; set; }
    public string RoundName { get; set; } = string.Empty;
    public RoundType RoundType { get; set; } = null!;
    public Game Game { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = null!;
}
