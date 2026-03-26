namespace ExerciseTracker.Application.Exercises;

public sealed class ExerciseHistoryItemDto
{
    public Guid Id { get; init; }
    public string ExerciseType { get; init; } = string.Empty;
    public int DurationMinutes { get; init; }
    public string Intensity { get; init; } = string.Empty;
    public DateTime PerformedOn { get; init; }
    public string? Notes { get; init; }
}
