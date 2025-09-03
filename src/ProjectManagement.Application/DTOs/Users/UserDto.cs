namespace ProjectManagement.Application.DTOs.Users;

public record UserDto(Guid Id, string Username, string Email, string PasswordHash);
