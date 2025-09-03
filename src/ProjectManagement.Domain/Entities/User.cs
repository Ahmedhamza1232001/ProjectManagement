namespace ProjectManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
