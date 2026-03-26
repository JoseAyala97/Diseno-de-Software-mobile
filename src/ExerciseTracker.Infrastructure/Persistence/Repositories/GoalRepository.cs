using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Persistence.Repositories;

public sealed class GoalRepository : IGoalRepository
{
    private readonly ApplicationDbContext _context;

    public GoalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Goal goal, CancellationToken cancellationToken = default)
    {
        var activeGoals = await _context.Goals
            .Where(x => x.UserId == goal.UserId && x.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var activeGoal in activeGoals)
        {
            activeGoal.Update(activeGoal.TargetValue, activeGoal.StartDate, activeGoal.EndDate, false);
        }

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Goal?> GetActiveByUserAsync(string userId, CancellationToken cancellationToken = default)
        => _context.Goals.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);

    public Task<Goal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _context.Goals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Goal>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Goals
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToArrayAsync(cancellationToken);
    }

    public async Task UpdateAsync(Goal goal, CancellationToken cancellationToken = default)
    {
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
