namespace ExerciseTracker.Application.Goals;

public sealed class GoalDto
{
    public Guid Id { get; init; }
    public string GoalType { get; init; } = string.Empty;
    public int TargetValue { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
}
