using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}