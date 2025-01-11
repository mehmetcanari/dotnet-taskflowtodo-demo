using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTO;

public record UpdateTodoDto
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Description { get; init; }
    [Required]
    public bool IsComplete { get; init; }
    [Required]
    public DateTime DueDate { get; init; }
}