using TodoItems.Models.Interfaces;

namespace TodoItems.Models.Entities
{
    public class TodoItem : ITodoItem, IBaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public long TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}