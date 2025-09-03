using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public decimal? Budget { get; set; }

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
