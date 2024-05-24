using Microsoft.EntityFrameworkCore;

namespace TodoApiMinimalNet8;
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TodoItem>  TodoItems { get; set; }
}
