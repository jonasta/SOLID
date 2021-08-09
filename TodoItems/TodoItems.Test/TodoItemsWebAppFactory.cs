using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TodoItems.Context.Context;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoItemService;
using TodoItems.Validation.TodoItem;

namespace TodoItems.Test
{
    public class TodoItemsWebAppFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers();
                services.AddDbContext<TodoContext>();
                services.AddTransient<ITodoItemService, TodoItemService>();
                services.AddTransient<IValidator<TodoItemPostDTO>, TodoItemPostValidator>();
            });
        }
    }
}