
namespace ProjectManagement.Application.DTOs.Tasks;

public record CreateTaskDto(string Title, string Description, Guid ProjectId);

