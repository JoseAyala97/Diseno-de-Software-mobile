namespace ExerciseTracker.Application.Statistics;

public sealed class ExerciseStatsDto
{
    public int TotalSessions { get; init; }
    public int TotalMinutes { get; init; }
    public double AverageMinutesPerSession { get; init; }
    public string MostFrequentExercise { get; init; } = "N/A";
    public IReadOnlyDictionary<string, int> SessionsByType { get; init; } = new Dictionary<string, int>();
}
