using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Abstractions.Services;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _userRepository.GetByIdAsync(id);

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto(u.Id, u.Email, u.Username, u.PasswordHash));
    }

    public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Username = dto.Username, 
            PasswordHash = dto.Password 
        };

        await _userRepository.AddAsync(user);

        return new UserDto(user.Id, user.Email, user.Username,user.PasswordHash);
    }

    public async Task<UserDto?> UpdateAsync(UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.Id);
        if (user is null) return null;

        user.Username = dto.UserName ?? user.Username; 
        user.Email = dto.Email ?? user.Email;

        await _userRepository.UpdateAsync(user);

        return new UserDto(user.Id, user.Email, user.Username,user.PasswordHash);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return false;

        await _userRepository.DeleteAsync(user,cancellationToken);
        return true;
    }
    public Task<User?> GetByEmailAsync(string email) =>
        _userRepository.GetByEmailAsync(email);

    
}
