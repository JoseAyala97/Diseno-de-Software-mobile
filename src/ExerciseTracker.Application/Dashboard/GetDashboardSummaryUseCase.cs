using ExerciseTracker.Application.Goals;
using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Domain.Enums;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Dashboard;

public sealed class GetDashboardSummaryUseCase
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IGoalRepository _goalRepository;
    private readonly IReminderRepository _reminderRepository;
    private readonly INotificationRepository _notificationRepository;

    public GetDashboardSummaryUseCase(
        IExerciseRepository exerciseRepository,
        IGoalRepository goalRepository,
        IReminderRepository reminderRepository,
        INotificationRepository notificationRepository)
    {
        _exerciseRepository = exerciseRepository;
        _goalRepository = goalRepository;
        _reminderRepository = reminderRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<DashboardSummaryDto> ExecuteAsync(
        string userId,
        string userName,
        CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.Date;
        var todayItems = await _exerciseRepository.GetByDateRangeAsync(userId, today, today.AddDays(1), cancellationToken);
        var recent = await _exerciseRepository.GetRecentByUserAsync(userId, 5, cancellationToken);
        var activeGoal = await _goalRepository.GetActiveByUserAsync(userId, cancellationToken);
        var reminders = await _reminderRepository.GetActiveByUserAsync(userId, cancellationToken);
        var unreadCount = await _notificationRepository.GetUnreadCountAsync(userId, cancellationToken);

        var goalDto = activeGoal is null
            ? null
            : new GoalDto
            {
                Id = activeGoal.Id,
                GoalType = activeGoal.GoalType.ToString(),
                TargetValue = activeGoal.TargetValue,
                StartDate = activeGoal.StartDate,
                EndDate = activeGoal.EndDate,
                IsActive = activeGoal.IsActive
            };

        var goalProgress = 0;
        if (activeGoal is not null)
        {
            var periodItems = await _exerciseRepository.GetByDateRangeAsync(userId, activeGoal.StartDate, activeGoal.EndDate.AddDays(1), cancellationToken);
            goalProgress = activeGoal.GoalType switch
            {
                GoalType.WeeklySessions => periodItems.Count,
                _ => periodItems.Sum(x => x.DurationMinutes)
            };
        }

        return new DashboardSummaryDto
        {
            UserName = userName,
            ExercisesToday = todayItems.Count,
            MinutesToday = todayItems.Sum(x => x.DurationMinutes),
            UnreadNotifications = unreadCount,
            ActiveGoal = goalDto,
            ActiveGoalProgress = goalProgress,
            NextReminder = reminders.OrderBy(x => x.ReminderTime).Select(x => $"{x.Title} - {x.ReminderTime:HH\\:mm}").FirstOrDefault(),
            RecentExercises = recent.Select(x => new ExerciseHistoryItemDto
            {
                Id = x.Id,
                ExerciseType = x.ExerciseType.ToString(),
                DurationMinutes = x.DurationMinutes,
                Intensity = x.Intensity.ToString(),
                PerformedOn = x.PerformedOn,
                Notes = x.Notes
            }).ToArray()
        };
    }
}
