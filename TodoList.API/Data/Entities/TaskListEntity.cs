using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.API.Data.Entities.BasicEntities;

namespace TodoList.API.Data.Entities;

public class TaskListEntity : IdentifiedEntity
{
    public required string Name { get; set; }
    public required Guid OwnerId { get; set; }
    public UserEntity Owner { get; set; } = null!;
    public DateTime? RemovedAt { get; set; }
    public List<TaskEntity> Tasks { get; set; } = new();
    public List<TaskListToUserLinkEntity> UserLinks { get; set; } = new();
}

public class TaskListEntityConfiguration : IdentifiedEntityConfiguration<TaskListEntity>
{
    protected override void ConfigureProperties(EntityTypeBuilder<TaskListEntity> builder)
    {
        base.ConfigureProperties(builder);
        builder.HasIndex(x => x.CreatedAt)
            .IsDescending(true);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.OwnedTasks)
            .HasForeignKey(x => x.OwnerId);
    }
}