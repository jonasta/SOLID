namespace TodoItems.Models.Interfaces
{
    public interface ITodoItem
    {
        string Name { get; set; }
        bool IsComplete { get; set; }
    }
}