using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<ICollection<TodoItem>>> GetTodoItems()
        {
            return Ok(await _service.GetTodoItemsAsync());
        }

        // GET: api/TodoItems/5
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

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _service.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

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

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };
            await _service.Insert(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        // DELETE: api/TodoItems/5
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
