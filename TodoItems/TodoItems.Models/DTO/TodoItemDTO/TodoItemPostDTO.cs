using TodoItems.Models.Interfaces;

namespace TodoItems.Models.DTO
{
    public class TodoItemPostDTO : ITodoItem
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public long TodoListId { get; set; }
    }
}