using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProjectManagementDbContext _db;

    public UserRepository(ProjectManagementDbContext db) => _db = db;

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
        => await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<List<User>> GetAllAsync()
        => await _db.Users.ToListAsync();

    public async Task<User?> GetByEmailAsync(string email)
        => await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
}
