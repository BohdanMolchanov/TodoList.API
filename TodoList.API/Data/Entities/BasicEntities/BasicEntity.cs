using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoList.API.Data.Entities.BasicEntities;

public class BasicEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
    public virtual int TableKey { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
}

public class BasicEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BasicEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigureProperties(builder);
        ConfigureDataSeeding(builder);
    }

    /// <summary>
    /// An entity's properties configuration.
    /// </summary>
    /// <param name="builder"></param>
    protected virtual void ConfigureProperties(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(f => f.TableKey);
        builder.Property(f => f.CreatedAt)
            .HasDefaultValueSql("now()::timestamp(0) at time zone 'utc'");
    }
        
    protected virtual void ConfigureDataSeeding(EntityTypeBuilder<T> builder)
    {
            
    }
}