using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.API.Data.Entities;

public class TaskListToUserLinkEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid TaskListId { get; set; }
    public TaskListEntity TaskList { get; set; }
}

public class TaskListToUserLinkEntityConfiguration : IEntityTypeConfiguration<TaskListToUserLinkEntity>
{
    public void Configure(EntityTypeBuilder<TaskListToUserLinkEntity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.TaskListId });
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.TaskListLinks)
            .HasForeignKey(x => x.UserId);
        
        builder.HasOne(x => x.TaskList)
            .WithMany(x => x.UserLinks)
            .HasForeignKey(x => x.TaskListId);
    }
}