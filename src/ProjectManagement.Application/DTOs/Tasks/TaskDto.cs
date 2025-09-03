namespace ProjectManagement.Application.DTOs.Tasks;

public record TaskDto(Guid Id, string Title, string Description, Guid ProjectId);
