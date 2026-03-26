using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Application.Goals;

namespace ExerciseTracker.Application.Dashboard;

public sealed class DashboardSummaryDto
{
    public string UserName { get; init; } = string.Empty;
    public int ExercisesToday { get; init; }
    public int MinutesToday { get; init; }
    public int UnreadNotifications { get; init; }
    public GoalDto? ActiveGoal { get; init; }
    public int ActiveGoalProgress { get; init; }
    public string? NextReminder { get; init; }
    public IReadOnlyCollection<ExerciseHistoryItemDto> RecentExercises { get; init; } = [];
}
