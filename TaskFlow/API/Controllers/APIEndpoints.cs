using TaskFlow.API.DTO;
using TaskFlow.API.Models;
using Microsoft.OpenApi.Models;

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
        app.MapGet("/api/todos", _tasksDb.GetTodoItems)
            .WithName("GetAllTodos")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets all todo items",
                Description = "Retrieves a list of all todo items in the database"
            });

        app.MapGet("/api/todos/{id}", _tasksDb.GetTodoItem)
            .WithName("GetTodoById")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets a todo item by ID",
                Description = "Retrieves a specific todo item by its ID"
            });
    }

    private void PostTodoItem()
    {
        app.MapPost("/api/todos", (CreateTodoDto createTodoDto) =>
        {
            TodoItem todoItem = new()
            {
                Id = _tasksDb.GetTodoItemsCount() + 1,    
                Title = createTodoDto.Title,
                Description = createTodoDto.Description,
            };
            
            if (createTodoDto.DueDate == DateTime.MinValue)
                return Results.BadRequest("DueDate is required");
            todoItem.DueDate = createTodoDto.DueDate;
            
            _tasksDb.AddTodoItem(todoItem);
            return Results.Created($"/api/todos/{todoItem.Id}", todoItem);
        })
            .WithName("CreateTodo")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Creates a new todo item",
                Description = "Creates a new todo item with the provided details"
            })
            .WithParameterValidation();
    }

    private void DeleteTodoItem()
    {
        app.MapDelete("/api/todos/{id}", (long id) =>
        {
            _tasksDb.DeleteTodoItem(id);
            return Results.Content($"Todo item {id} deleted successfully");
        })
            .WithName("DeleteTodo")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Deletes a todo item",
                Description = "Deletes a specific todo item by its ID"
            });
    }

    private void PatchTodoItem()
    {
        app.MapPatch("/api/todos/{id}", (long id, PatchTodoDto patchTodoDto) =>
        {
            TodoItem todoItem = _tasksDb.GetTodoItem(id);
            todoItem.Title = patchTodoDto.Title;
            todoItem.Description = patchTodoDto.Description;
            todoItem.IsComplete = patchTodoDto.IsComplete;
            
            return Results.Ok(todoItem);
        })
            .WithName("UpdateTodoStatus")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Updates todo completion status",
                Description = "Updates the completion status of a specific todo item"
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
            .WithName("UpdateTodo")
            .WithTags("Todos")
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Updates a todo item",
                Description = "Updates all fields of a specific todo item"
            })
            .WithParameterValidation();
    }
}