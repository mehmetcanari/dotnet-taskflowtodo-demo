using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTO;

public record PatchTodoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public bool IsComplete { get; set; }
}