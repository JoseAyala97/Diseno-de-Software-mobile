using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Notifications;

public sealed class MarkNotificationAsReadUseCase
{
    private readonly INotificationRepository _notificationRepository;

    public MarkNotificationAsReadUseCase(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<bool> ExecuteAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId, cancellationToken);
        if (notification is null)
        {
            return false;
        }

        notification.MarkAsRead();
        await _notificationRepository.UpdateAsync(notification, cancellationToken);
        return true;
    }
}
