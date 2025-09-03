using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Application.Abstractions.Services;

namespace ProjectManagement.Infrastructure.Jobs;

public class OverdueTaskNotificationJob
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEmailService _emailService;

    public OverdueTaskNotificationJob(ITaskRepository taskRepository, IEmailService emailService)
    {
        _taskRepository = taskRepository;
        _emailService = emailService;
    }

    public async Task ExecuteAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        var overdueTasks = tasks.Where(t => t.DueDate < DateTime.UtcNow && t.Status != Domain.Enums.TaskStatus.Completed);

        foreach (var task in overdueTasks)
        {
            await _emailService.SendEmailAsync(task.AssignedTo?.ToString() ?? string.Empty, "Task overdue", $"Task {task.Title} is overdue!");
        }
    }
}
