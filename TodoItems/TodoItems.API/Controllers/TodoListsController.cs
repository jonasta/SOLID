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
        public async Task<IActionResult> GetTodoLists()
        {
            return Ok(await _service.GetTodoListsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoList(long id)
        {
            var todoList = await _service.GetTodoListAsync(id);
            if (todoList == null) return NotFound();
            return Ok(todoList);
        }

        [HttpPost, ValidateModel]
        public async Task<IActionResult> PostTodoList(TodoListPostDTO todoListPostDTO)
        {
            var newId = await _service.Insert(todoListPostDTO);
            return CreatedAtAction(nameof(GetTodoList), new { id = newId }, todoListPostDTO);
        }

        [HttpPut("{id}"), ValidateModel]
        public async Task<IActionResult> PutTodoList(long id, TodoListDTO todoListDTO)
        {
            await _service.UpdateAsync(id, todoListDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(long id)
        {
            var todoList = await _service.GetTodoListAsync(id);
            if (todoList == null) return NotFound();
            await _service.Delete(todoList.Id);
            return NoContent();
        }
    }
}