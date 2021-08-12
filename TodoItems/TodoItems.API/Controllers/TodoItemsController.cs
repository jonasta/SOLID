using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.API.Attributes;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;
using TodoItems.Service.TodoItemService;

namespace TodoItems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TodoItem>>> GetTodoItems()
        {
            return Ok(await _service.GetTodoItemsAsync());
        }

        [HttpGet("/api/TodoLists{listId}/TodoItems")]
        public async Task<ActionResult<ICollection<TodoItem>>> GetTodoItemsByListId(long listId)
        {
            return Ok(await _service.GetTodoItemsAsync());
        }

        [HttpGet("/api/TodoLists{listId}/TodoItems{todoItemId}")]
        public async Task<ActionResult<TodoItem>> GetTodoItemByListId(long todoListId, long todoItemId)
        {
            var todoItem = await _service.GetTodoItemAsync(todoItemId);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost("/api/TodoLists{listId}/TodoItems"), ValidateModel]
        public async Task<ActionResult<TodoItemPostDTO>> PostTodoItem(long todoListId, TodoItemPostDTO todoItemPostDTO)
        {
            await _service.Insert(todoItemPostDTO);
            return NoContent();
        }

        [HttpPut("/api/TodoLists{listId}/TodoItems{todoItemId}")]
        public async Task<IActionResult> PutTodoItem(long todoListId, long todoItemId, TodoItemPutDTO todoItemDTO)
        {
            var todoItem = await _service.GetTodoItemAsync(todoItemId);
            try
            {
                await _service.UpdateAsync(todoItem, todoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (_service.GetTodoItemAsync(todoItemId).Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/api/TodoLists{listId}/TodoItems{todoItemId}")]
        public async Task<IActionResult> DeleteTodoItem(long todoListId, long todoItemId)
        {
            var TodoItem = await _service.GetTodoItemAsync(todoItemId);
            if (TodoItem == null)
            {
                return NotFound();
            }
            await _service.Delete(TodoItem);
            return NoContent();
        }
    }
}