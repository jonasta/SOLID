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

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _service.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemPutDTO todoItemDTO)
        {
            var todoItem = await _service.GetTodoItemAsync(id);
            try
            {
                await _service.UpdateAsync(todoItem, todoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (_service.GetTodoItemAsync(id).Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost, ValidateModel]
        public async Task<ActionResult<TodoItemPostDTO>> PostTodoItem(TodoItemPostDTO todoItemPostDTO)
        {
            await _service.Insert(todoItemPostDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var TodoItem = await _service.GetTodoItemAsync(id);
            if (TodoItem == null)
            {
                return NotFound();
            }
            await _service.Delete(TodoItem);
            return NoContent();
        }
    }
}