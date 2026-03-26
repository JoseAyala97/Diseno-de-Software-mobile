namespace ExerciseTracker.Application.Goals;

public sealed record UpdateGoalRequest(
    Guid GoalId,
    int TargetValue,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive);
