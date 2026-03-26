using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Persistence.Repositories;

public sealed class ExerciseRepository : IExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ExerciseRecord exerciseRecord, CancellationToken cancellationToken = default)
    {
        _context.ExerciseRecords.Add(exerciseRecord);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ExerciseRecord>> GetByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.ExerciseRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.PerformedOn >= startDate && x.PerformedOn < endDate)
            .OrderByDescending(x => x.PerformedOn)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ExerciseRecord>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.ExerciseRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.PerformedOn)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ExerciseRecord>> GetRecentByUserAsync(string userId, int take, CancellationToken cancellationToken = default)
    {
        return await _context.ExerciseRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.PerformedOn)
            .Take(take)
            .ToArrayAsync(cancellationToken);
    }
}
