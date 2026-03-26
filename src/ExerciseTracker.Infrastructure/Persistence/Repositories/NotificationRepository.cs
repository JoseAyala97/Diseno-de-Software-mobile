using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Persistence.Repositories;

public sealed class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Notifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<int> GetUnreadCountAsync(string userId, CancellationToken cancellationToken = default)
        => _context.Notifications.CountAsync(x => x.UserId == userId && !x.IsRead, cancellationToken);

    public async Task<IReadOnlyCollection<Notification>> GetRecentByUserAsync(string userId, int take, CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(take)
            .ToArrayAsync(cancellationToken);
    }

    public async Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
