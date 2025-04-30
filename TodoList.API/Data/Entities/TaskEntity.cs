using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.API.Data.Entities.BasicEntities;

namespace TodoList.API.Data.Entities;

public class TaskEntity : IdentifiedEntity
{
    public required Guid TaskListId { get; set; }
    public TaskListEntity TaskList { get; set; } = null!;
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskEntityConfiguration : IdentifiedEntityConfiguration<TaskEntity>
{
    protected override void ConfigureProperties(EntityTypeBuilder<TaskEntity> builder)
    {
        base.ConfigureProperties(builder);
        builder.HasIndex(x => x.IsCompleted)
            .IsDescending(false);
    }
}