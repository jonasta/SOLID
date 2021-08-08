using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoItems.Models.Interfaces
{
    public interface ITodoItem
    {
        long Id { get; set; }
        string Name { get; set; }
        bool IsComplete { get; set; }
    }
}
