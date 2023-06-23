using System.ComponentModel.DataAnnotations;

namespace MohaQuiz.Backend.Models.DTOs;

public class TeamNameDTO
{
    [Required]
    public string TeamName { get; set; } = string.Empty;
}
