using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Persistence.Configurations;

public class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
{
    public void Configure(EntityTypeBuilder<TaskAttachment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(a => a.ContentType)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.Data)
               .IsRequired();

        builder.HasOne(a => a.TaskItem)
               .WithMany(t => t.Attachments)
               .HasForeignKey(a => a.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
