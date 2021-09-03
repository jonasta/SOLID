using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoItems.Context.Context;
using TodoItems.Models.AutoMapperProfiles;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoItemService;
using TodoItems.Service.TodoListService;
using TodoItems.Service.Validation;
using TodoItems.Validation;

namespace TodoItems.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>();

            services.AddAutoMapper(typeof(TodoItemProfile), typeof(TodoListProfile));

            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoItems.API", Version = "v1" });
            });
            AddTransients(services);
            AddValidators(services);
            //services.AddDatabaseDeveloperPageExceptionFilter();
        }

        private void AddTransients(IServiceCollection services)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<ITodoListService, TodoListService>();
        }

        private void AddValidators(IServiceCollection services)
        {
            services.AddTransient<ITodoItemValidatorService, TodoItemValidatorService>();
            services.AddTransient<IValidator<TodoItemPostDTO>, TodoItemPostValidator>();
            services.AddTransient<IValidator<TodoItemDTO>, TodoItemPutValidator>();

            services.AddTransient<ITodoListValidatorService, TodoListValidatorService>();
            services.AddTransient<IValidator<TodoListPostDTO>, TodoListPostValidator>();
            services.AddTransient<IValidator<TodoListDTO>, TodoListPutValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoItems.API v1"));
            }
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Cache-Control", "no-store, no-cache");
                await next();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}