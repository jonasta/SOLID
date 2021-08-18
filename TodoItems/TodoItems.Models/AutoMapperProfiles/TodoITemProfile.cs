using AutoMapper;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Models.AutoMapperProfiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
            CreateMap<TodoItem, TodoItemPostDTO>().ReverseMap();
        }
    }
}