using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> dbContextOptions): base(dbContextOptions)
    {
    }
    
    public DbSet<TodoItem> TodoItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TodoItem>()
            .Property(item => item.CreatedAt)
            .HasConversion(
                v => v.ToUtc(),
                v => v.ToUtc());
    }
}