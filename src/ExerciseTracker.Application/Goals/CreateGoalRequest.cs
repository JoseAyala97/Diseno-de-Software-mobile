using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Application.Goals;

public sealed record CreateGoalRequest(
    string UserId,
    GoalType GoalType,
    int TargetValue,
    DateTime StartDate,
    DateTime EndDate);
