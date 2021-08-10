using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoItems.Context.Context;
using System;
using TodoItems.Service.TodoItemService;
using FluentValidation;
using TodoItems.Models.DTO;
using TodoItems.Validation.TodoItem;
using FluentValidation.AspNetCore;

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
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<TodoContext>();

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
        }

        private void AddValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<TodoItemPostDTO>, TodoItemPostValidator>();
            services.AddTransient<IValidator<TodoItemPutDTO>, TodoItemPutValidator>();
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