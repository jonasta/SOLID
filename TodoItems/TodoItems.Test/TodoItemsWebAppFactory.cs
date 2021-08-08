using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoItems.Service.TodoItemService;
using Microsoft.Extensions.DependencyInjection;
using TodoItems.Context.Context;

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
            });
        }
    }
}
