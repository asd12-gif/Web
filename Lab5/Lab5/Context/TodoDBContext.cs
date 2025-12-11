using Lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Context
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }

        public DbSet<TodoItems> ToDoItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItems>().HasData(
                new TodoItems { Id = 1, Name = "ahihi", IsComplete = false },
                new TodoItems { Id = 2, Name = "ahaha", IsComplete = true }
            );
        }
    }
}
