namespace ExerciseTracker.Application.Notifications;

public sealed record CreateReminderRequest(string UserId, string Title, string Message, TimeOnly ReminderTime);
