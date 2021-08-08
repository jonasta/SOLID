using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoItems.Models.DTO;
using System;

namespace TodoItems.Service.TodoItemService
{
    public interface ITodoItemService
    {

        ValueTask<List<TodoItem>> GetTodoItemsAsync();

        ValueTask<TodoItem> GetTodoItemAsync(long id);

        ValueTask<bool> UpdateAsync(TodoItem todoItem, TodoItemDTO todoItemDTO);

        ValueTask<bool> Insert(TodoItem todoItem);

        ValueTask<bool> Delete(TodoItem todoItem);

    }
}
