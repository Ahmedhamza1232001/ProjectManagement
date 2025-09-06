using MediatR;
using ProjectManagement.Application.DTOs.Users;
using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Abstractions.Services;

namespace ProjectManagement.Application.Features.Users.Commands;

public record RefreshTokensCommand(Guid UserId, string RefreshToken) : IRequest<TokensDto?>;

public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, TokensDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public RefreshTokensCommandHandler(
        IUserRepository userRepository,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<TokensDto?> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        // 1. Get user by Id
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return null;

        // 2. Validate refresh token
        var isValid = await _authService.ValidateRefreshTokenAsync(user, request.RefreshToken, cancellationToken);
        if (!isValid)
            return null;

        // 3. Generate new tokens
        var (accessToken, refreshToken) = _authService.GenerateTokens(user, cancellationToken);

        // 4. Map tuple to DTO
        return new TokensDto(accessToken, refreshToken);
    }
}
