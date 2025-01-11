namespace TaskFlow.API.DTO;

public record PatchTodoDto
{
    public bool IsComplete { get; set; }
}