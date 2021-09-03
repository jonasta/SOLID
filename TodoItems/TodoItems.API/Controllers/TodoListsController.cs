using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoListService;

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
        public async Task<IActionResult> GetTodoLists()
        {
            return Ok(await _service.GetTodoListsAsync());
        }

        [HttpGet("/api/TodoList({id})")]
        public async Task<IActionResult> GetTodoList(long id)
        {
            var todoList = await _service.GetTodoListAsync(id);
            if (todoList is null)
                return NotFound();
            else
                return Ok(todoList);
        }

        [HttpPost("/api/TodoList")]
        public async Task<IActionResult> PostTodoList(TodoListPostDTO todoListPostDTO)
        {
            var todoItem = await _service.InsertAsync(todoListPostDTO);
            return CreatedAtAction(nameof(GetTodoList), new { id = todoItem.Id }, todoListPostDTO);
        }

        [HttpPut("/api/TodoList({id})")]
        public async Task<IActionResult> PutTodoList(long id, TodoListDTO todoListDTO)
        {
            await _service.UpdateAsync(id, todoListDTO);
            return NoContent();
        }

        [HttpDelete("/api/TodoList({id})")]
        public async Task<IActionResult> DeleteTodoList(long id)
        {
            var todoList = await _service.GetTodoListAsync(id);
            if (todoList == null) return NotFound();
            await _service.DeleteAsync(todoList.Id);
            return NoContent();
        }
    }
}