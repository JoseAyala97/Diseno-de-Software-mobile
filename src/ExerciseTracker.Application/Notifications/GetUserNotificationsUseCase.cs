using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Notifications;

public sealed class GetUserNotificationsUseCase
{
    private readonly IReminderRepository _reminderRepository;
    private readonly INotificationRepository _notificationRepository;

    public GetUserNotificationsUseCase(IReminderRepository reminderRepository, INotificationRepository notificationRepository)
    {
        _reminderRepository = reminderRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<(IReadOnlyCollection<ReminderDto> Reminders, IReadOnlyCollection<NotificationDto> Notifications)> ExecuteAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var reminders = await _reminderRepository.GetActiveByUserAsync(userId, cancellationToken);
        var notifications = await _notificationRepository.GetRecentByUserAsync(userId, 10, cancellationToken);

        return (
            reminders.Select(x => new ReminderDto
            {
                Id = x.Id,
                Title = x.Title,
                Message = x.Message,
                ReminderTime = x.ReminderTime.ToString("HH:mm"),
                IsActive = x.IsActive
            }).ToArray(),
            notifications.Select(x => new NotificationDto
            {
                Id = x.Id,
                Title = x.Title,
                Message = x.Message,
                IsRead = x.IsRead,
                CreatedAtUtc = x.CreatedAtUtc
            }).ToArray());
    }
}
