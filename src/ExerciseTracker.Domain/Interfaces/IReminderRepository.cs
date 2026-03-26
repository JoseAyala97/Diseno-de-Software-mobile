using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Interfaces;

public interface IReminderRepository
{
    Task AddAsync(Reminder reminder, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Reminder>> GetActiveByUserAsync(string userId, CancellationToken cancellationToken = default);
}
