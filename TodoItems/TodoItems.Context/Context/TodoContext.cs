using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoItems.Models.Entities;

namespace TodoItems.Context.Context
{
    public class TodoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public TodoContext(DbContextOptions<TodoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
        }
    }
}
