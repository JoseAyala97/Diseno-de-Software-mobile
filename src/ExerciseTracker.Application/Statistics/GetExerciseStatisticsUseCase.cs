using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Statistics;

public sealed class GetExerciseStatisticsUseCase
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseStatisticsUseCase(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ExerciseStatsDto> ExecuteAsync(
        string userId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        var items = await _exerciseRepository.GetByDateRangeAsync(userId, startDate.Date, endDate.Date.AddDays(1), cancellationToken);
        var grouped = items
            .GroupBy(x => x.ExerciseType.ToString())
            .ToDictionary(x => x.Key, x => x.Count());

        return new ExerciseStatsDto
        {
            TotalSessions = items.Count,
            TotalMinutes = items.Sum(x => x.DurationMinutes),
            AverageMinutesPerSession = items.Count == 0 ? 0 : items.Average(x => x.DurationMinutes),
            MostFrequentExercise = grouped.OrderByDescending(x => x.Value).Select(x => x.Key).FirstOrDefault() ?? "N/A",
            SessionsByType = grouped
        };
    }
}
