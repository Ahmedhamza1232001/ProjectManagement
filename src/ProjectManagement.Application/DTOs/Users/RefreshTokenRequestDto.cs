namespace ProjectManagement.Application.DTOs.Users;

public record RefreshTokenRequestDto(Guid UserId, string RefreshToken);