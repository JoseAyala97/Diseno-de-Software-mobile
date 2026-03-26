using ExerciseTracker.Domain.Common;

namespace ExerciseTracker.Domain.Entities;

public sealed class Reminder : BaseEntity
{
    private Reminder()
    {
    }

    public Reminder(string userId, string title, string message, TimeOnly reminderTime)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        UserId = userId;
        Title = title.Trim();
        Message = message.Trim();
        ReminderTime = reminderTime;
        IsActive = true;
    }

    public string UserId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public TimeOnly ReminderTime { get; private set; }
    public bool IsActive { get; private set; }

    public void Deactivate() => IsActive = false;
}
