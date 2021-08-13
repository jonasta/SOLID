using System.Collections.Generic;
using TodoItems.Models.Entities;
using TodoItems.Models.Interfaces;

namespace TodoItems.Models.DTO
{
    public class TodoListPostDTO : ITodoList
    {
        public string Name { get; set; }
    }
}