using Microsoft.EntityFrameworkCore;
using todolist.Models;

namespace todolist.infraestructura
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        { }
            public DbSet<TodoList> TodoList { get; set; }
        

    }
}
