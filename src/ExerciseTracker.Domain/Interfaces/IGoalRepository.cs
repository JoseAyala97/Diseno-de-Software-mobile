using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Interfaces;

public interface IGoalRepository
{
    Task AddAsync(Goal goal, CancellationToken cancellationToken = default);
    Task UpdateAsync(Goal goal, CancellationToken cancellationToken = default);
    Task<Goal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Goal?> GetActiveByUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Goal>> GetByUserAsync(string userId, CancellationToken cancellationToken = default);
}
