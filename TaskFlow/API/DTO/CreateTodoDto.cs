using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTO;

public record CreateTodoDto
{
    [Required][MaxLength(50)]
    public string Title { get; init; }
    [MaxLength(200)]
    public string Description { get; init; }
    [Required]
    public DateTime DueDate { get; init; }
}