namespace TaskFlow.API.Models;

public class TasksDatabase
{
    private readonly List<TodoItem> _todoItems = [];
    
    public void AddTodoItem(TodoItem? todoItem)
    {
        if (todoItem == null)
        {
            throw new Exception("Todo item is null");
        }
        
        _todoItems.Add(todoItem);
    }

    public int GetTodoItemsCount()
    {
        return _todoItems.Count;
    }
    
    public List<TodoItem?> GetTodoItems()
    {
        if (_todoItems.Count == 0)
        {
            throw new Exception("No todo items found");
        }
        
        return _todoItems;
    }

    public TodoItem? GetTodoItem(long id)
    {
        TodoItem item = _todoItems.FirstOrDefault(todoItem => todoItem.Id == id);
        if (item == null)
        {
            throw new Exception("Item not found");
        }
        
        return item;
    }
    
    public void DeleteTodoItem(long id)
    {
        TodoItem item = _todoItems.FirstOrDefault(todoItem => todoItem.Id == id);
        if (item == null)
        {
            throw new Exception("Item not found");
        }
        
        _todoItems.Remove(item);
    }
}