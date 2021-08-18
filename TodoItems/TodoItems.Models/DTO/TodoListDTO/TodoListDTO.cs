using System.Collections.Generic;
using TodoItems.Models.Entities;
using TodoItems.Models.Interfaces;

namespace TodoItems.Models.DTO
{
    public class TodoListDTO : ITodoList
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}