namespace TaskFlow.API.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
}