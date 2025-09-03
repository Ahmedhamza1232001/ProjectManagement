using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Persistence;

public class ProjectManagementDbContext : DbContext
{
    public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
        : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
}
