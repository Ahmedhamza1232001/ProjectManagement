using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Domain.Entities;

public class TaskItem : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public Guid? AssignedToUserId { get; set; }
    public User? AssignedTo { get; set; }

    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public Enums.TaskStatus Status { get; set; }

    public ICollection<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();
}
