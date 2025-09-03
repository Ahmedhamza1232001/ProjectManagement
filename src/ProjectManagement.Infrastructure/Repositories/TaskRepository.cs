using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ProjectManagementDbContext _db;

    public TaskRepository(ProjectManagementDbContext db) => _db = db;

    public async Task AddAsync(TaskItem task)
    {
        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _db.Tasks.Update(task);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
        => await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<List<TaskItem>> GetAllAsync()
        => await _db.Tasks.ToListAsync();

    public async Task<List<TaskItem>> SearchAsync(string keyword)
    => await _db.Tasks
        .Where(t => t.Title.Contains(keyword) || (t.Description ?? "").Contains(keyword))
        .ToListAsync();
}
