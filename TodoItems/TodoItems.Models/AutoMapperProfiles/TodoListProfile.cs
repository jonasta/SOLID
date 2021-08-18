using AutoMapper;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Models.AutoMapperProfiles
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListDTO>().ReverseMap();
            CreateMap<TodoList, TodoListPostDTO>().ReverseMap();
        }
    }
}