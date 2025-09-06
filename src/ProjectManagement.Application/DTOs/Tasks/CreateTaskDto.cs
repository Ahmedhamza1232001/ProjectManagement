
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.DTOs.Tasks;

public record CreateTaskDto(string Title, string Description, Guid ProjectId, User AssignedTo);

