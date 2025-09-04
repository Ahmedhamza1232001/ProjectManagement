namespace ProjectManagement.Application.DTOs.Users;

public record UpdateUserDto(Guid Id, string? UserName, string? Email, string? Password);
