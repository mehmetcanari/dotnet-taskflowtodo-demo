using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTO;

public record PatchTodoDto
{
    public bool IsComplete { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}