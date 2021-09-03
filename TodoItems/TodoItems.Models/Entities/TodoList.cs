using System.Collections.Generic;
using TodoItems.Models.Interfaces;

namespace TodoItems.Models.Entities
{
    public class TodoList : ITodoList, IBaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TodoItem> Items { get; set; }
    }
}