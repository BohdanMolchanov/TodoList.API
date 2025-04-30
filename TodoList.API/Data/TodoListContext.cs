using Microsoft.EntityFrameworkCore;
using TodoList.API.Data.Entities;

namespace TodoList.API.Data;

public class TodoListContext(DbContextOptions<TodoListContext> options): DbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskListEntity> TaskLists { get; set; }
    public DbSet<TaskListToUserLinkEntity> TaskListToUserLinks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TaskListEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TaskListToUserLinkEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}