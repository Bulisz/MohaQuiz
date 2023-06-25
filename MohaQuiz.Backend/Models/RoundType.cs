﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MohaQuiz.Backend.Models;

[Table("RoundTypes")]
public class RoundType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public ICollection<Round> Rounds { get; set; }
}
