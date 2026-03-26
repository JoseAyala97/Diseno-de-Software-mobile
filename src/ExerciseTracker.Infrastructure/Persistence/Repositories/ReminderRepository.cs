using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Persistence.Repositories;

public sealed class ReminderRepository : IReminderRepository
{
    private readonly ApplicationDbContext _context;

    public ReminderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Reminder reminder, CancellationToken cancellationToken = default)
    {
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Reminder>> GetActiveByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Reminders
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.IsActive)
            .OrderBy(x => x.ReminderTime)
            .ToArrayAsync(cancellationToken);
    }
}
