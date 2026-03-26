namespace ExerciseTracker.Application.Notifications;

public sealed class ReminderDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string ReminderTime { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
