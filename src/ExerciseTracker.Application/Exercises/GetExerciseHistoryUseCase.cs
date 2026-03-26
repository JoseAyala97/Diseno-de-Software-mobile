using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Exercises;

public sealed class GetExerciseHistoryUseCase
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseHistoryUseCase(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<IReadOnlyCollection<ExerciseHistoryItemDto>> ExecuteAsync(string userId, CancellationToken cancellationToken = default)
    {
        var items = await _exerciseRepository.GetByUserAsync(userId, cancellationToken);
        return items
            .OrderByDescending(x => x.PerformedOn)
            .Select(x => new ExerciseHistoryItemDto
            {
                Id = x.Id,
                ExerciseType = x.ExerciseType.ToString(),
                DurationMinutes = x.DurationMinutes,
                Intensity = x.Intensity.ToString(),
                PerformedOn = x.PerformedOn,
                Notes = x.Notes
            })
            .ToArray();
    }
}
