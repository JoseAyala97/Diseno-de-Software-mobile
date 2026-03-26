using ExerciseTracker.Application.Statistics;

namespace ExerciseTracker.Web.ViewModels;

public sealed class StatsViewModel
{
    public DateTime StartDate { get; init; } = DateTime.Today.AddDays(-7);
    public DateTime EndDate { get; init; } = DateTime.Today;
    public ExerciseStatsDto Summary { get; init; } = new();
}
