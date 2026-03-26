using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Notifications;

public sealed class CreateReminderUseCase
{
    private readonly IReminderRepository _reminderRepository;
    private readonly INotificationRepository _notificationRepository;

    public CreateReminderUseCase(IReminderRepository reminderRepository, INotificationRepository notificationRepository)
    {
        _reminderRepository = reminderRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<ReminderDto> ExecuteAsync(CreateReminderRequest request, CancellationToken cancellationToken = default)
    {
        var reminder = new Reminder(request.UserId, request.Title, request.Message, request.ReminderTime);
        await _reminderRepository.AddAsync(reminder, cancellationToken);
        await _notificationRepository.AddAsync(
            new Domain.Entities.Notification(request.UserId, "Recordatorio creado", $"Se programo el recordatorio \"{request.Title}\"."),
            cancellationToken);

        return new ReminderDto
        {
            Id = reminder.Id,
            Title = reminder.Title,
            Message = reminder.Message,
            ReminderTime = reminder.ReminderTime.ToString("HH:mm"),
            IsActive = reminder.IsActive
        };
    }
}
