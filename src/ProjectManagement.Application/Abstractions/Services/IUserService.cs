// IUserService.cs
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Abstractions.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> RegisterAsync(RegisterUserDto dto);
    Task<UserDto> UpdateAsync(UpdateUserDto dto);
    Task<bool> DeleteAsync(Guid id,CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email);
}
