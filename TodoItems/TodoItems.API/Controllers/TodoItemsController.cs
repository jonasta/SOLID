using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
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
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await _service.GetTodoItemsAsync());
        }

        [HttpGet("/api/TodoLists({listId})/TodoItems")]
        public async Task<IActionResult> GetTodoItemsByListId(long listId)
        {
            return Ok(await _service.GetTodoItemsByListIdAsync(listId));
        }

        [HttpGet("/api/TodoLists({listId})/TodoItems({todoItemId})")]
        public async Task<IActionResult> GetTodoItemByListId(long listId, long todoItemId)
        {
            var todoItem = await _service.GetTodoItemByListIdAsync(listId, todoItemId);
            return Ok(todoItem);
        }

        [HttpPost("/api/TodoLists({listId})/TodoItems")]
        public async Task<IActionResult> PostTodoItem(long listId, TodoItemPostDTO todoItemPostDTO)
        {
            todoItemPostDTO.TodoListId = listId;
            var todoItem = await _service.InsertAsync(todoItemPostDTO);
            return CreatedAtAction(nameof(GetTodoItemByListId), new { listId = todoItem.TodoListId, todoItemId = todoItem.Id }, todoItemPostDTO);
        }

        [HttpPut("/api/TodoLists({listId})/TodoItems({todoItemId})")]
        public async Task<IActionResult> PutTodoItem(long listId, long todoItemId, TodoItemDTO todoItemDTO)
        {
            await _service.UpdateAsync(todoItemId, todoItemDTO);
            return NoContent();
        }

        [HttpDelete("/api/TodoLists({listId})/TodoItems({todoItemId})")]
        public async Task<IActionResult> DeleteTodoItem(long listId, long todoItemId)
        {
            var todoItem = await _service.GetTodoItemByListIdAsync(listId, todoItemId);
            if (todoItem == null) return NotFound();

            await _service.DeleteAsync(todoItem.Id);
            return NoContent();
        }
    }
}