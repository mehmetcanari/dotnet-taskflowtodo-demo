using TaskFlow.API.DTO;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers;

public class ApiEndpoints(WebApplication app)
{
    private readonly TasksDatabase _tasksDb = new();
    
    public void Initialize()
    {
        GetTodoItems();
        PostTodoItem();
        DeleteTodoItem();
        PatchTodoItem();
        UpdateTodoItem();
    }

    private void GetTodoItems()
    {
        app.MapGet("/", () => Results.Text("Welcome to TaskFlow API!"));
        app.MapGet("/api/todos", _tasksDb.GetTodoItems);
        app.MapGet("/api/todos/{id}", _tasksDb.GetTodoItem);
    }

    private void PostTodoItem()
    {
        app.MapPost("/api/todos", (CreateTodoDto todoDto) =>
        {
            TodoItem todoItem = new()
            {
                Id = _tasksDb.GetTodoItemsCount() + 1,    
                Title = todoDto.Title,
                Description = todoDto.Description,
                DueDate = todoDto.DueDate,
            };
            
            _tasksDb.AddTodoItem(todoItem);
            return Results.Created($"/api/todos/{todoItem.Id}", todoItem);
        })
            .WithParameterValidation();
    }

    private void DeleteTodoItem()
    {
        app.MapDelete("/api/todos/{id}", (long id) =>
        {
            _tasksDb.DeleteTodoItem(id);
            return Results.Content($"Todo item {id} deleted successfully");
        });
    }

    private void PatchTodoItem()
    {
        app.MapPatch("/api/todos/{id}", (long id, PatchTodoDto patchTodoDto) =>
        {
            TodoItem todoItem = _tasksDb.GetTodoItem(id);
            todoItem.IsComplete = patchTodoDto.IsComplete;
            
            return Results.Ok(todoItem);
        })
            .WithParameterValidation();
    }

    private void UpdateTodoItem()
    {
        app.MapPut("/api/todos/{id}", (long id, UpdateTodoDto updateTodoDto) =>
        {
            TodoItem todoItem = _tasksDb.GetTodoItem(id);
            
            todoItem.Title = updateTodoDto.Title;
            todoItem.Description = updateTodoDto.Description;
            todoItem.IsComplete = updateTodoDto.IsComplete;
           
            if (updateTodoDto.DueDate == DateTime.MinValue)
                return Results.BadRequest("DueDate is required");
            todoItem.DueDate = updateTodoDto.DueDate;
            
            return Results.Ok(todoItem);
        })
            .WithParameterValidation();
    }
}