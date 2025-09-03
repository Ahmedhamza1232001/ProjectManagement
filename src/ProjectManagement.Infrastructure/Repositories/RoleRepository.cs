using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Persistence;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ProjectManagementDbContext _db;

        public RoleRepository(ProjectManagementDbContext db) => _db = db;

        public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
        {
            await _db.Roles.AddAsync(role, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
        {
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Roles.ToListAsync(cancellationToken);
        }
    }
}
