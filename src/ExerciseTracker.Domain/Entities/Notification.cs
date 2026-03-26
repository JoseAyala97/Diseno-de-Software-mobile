using ExerciseTracker.Domain.Common;

namespace ExerciseTracker.Domain.Entities;

public sealed class Notification : BaseEntity
{
    private Notification()
    {
    }

    public Notification(string userId, string title, string message)
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
        IsRead = false;
    }

    public string UserId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public bool IsRead { get; private set; }

    public void MarkAsRead() => IsRead = true;
}
