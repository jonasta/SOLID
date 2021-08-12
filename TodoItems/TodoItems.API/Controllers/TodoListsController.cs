using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.API.Attributes;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoListService;
using System.Linq;

namespace TodoItems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly ITodoListService _service;

        public TodoListsController(ITodoListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TodoListPutDTO>>> GetTodoLists()
        {
            return Ok((await _service.GetTodoListsAsync()).Select(todoList => new TodoListPutDTO() { Name = todoList.Name, Id = todoList.Id }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListPutDTO>> GetTodoList(long id)
        {
            var todoList = await _service.GetTodoListAsync(id);
            return todoList == null ? NotFound() : Ok(new TodoListPutDTO() { Name = todoList.Name, Id = todoList.Id });
        }

        [HttpPost, ValidateModel]
        public async Task<ActionResult<TodoListPostDTO>> PostTodoList(TodoListPostDTO todoListPostDTO)
        {
            var newId = await _service.Insert(todoListPostDTO);
            return CreatedAtAction(nameof(GetTodoList), new { id = newId }, todoListPostDTO);
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> PutTodoList(long id, TodoListPutDTO todoListDTO)
        {
            await _service.UpdateAsync(id, todoListDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(long id)
        {
            var TodoList = await _service.GetTodoListAsync(id);
            if (TodoList == null) return NotFound();
            await _service.Delete(TodoList);
            return NoContent();
        }
    }
}