namespace ProjectManagement.Application.DTOs.Tasks;

public record UpdateTaskDto(Guid Id, string Title, string Description, Guid ProjectId);
