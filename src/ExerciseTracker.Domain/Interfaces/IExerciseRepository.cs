using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Interfaces;

public interface IExerciseRepository
{
    Task AddAsync(ExerciseRecord exerciseRecord, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ExerciseRecord>> GetByUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ExerciseRecord>> GetByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ExerciseRecord>> GetRecentByUserAsync(string userId, int take, CancellationToken cancellationToken = default);
}
