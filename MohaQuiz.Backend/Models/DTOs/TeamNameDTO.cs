using System.ComponentModel.DataAnnotations;

namespace MohaQuiz.Backend.Models.DTOs;

public record TeamNameDTO
{
    [Required]
    public string TeamName { get; set; } = string.Empty;
}
