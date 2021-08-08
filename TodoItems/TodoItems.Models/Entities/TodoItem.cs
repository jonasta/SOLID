using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoItems.Models.Interfaces;

namespace TodoItems.Models.Entities
{
    public class TodoItem : ITodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
