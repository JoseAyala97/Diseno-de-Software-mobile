using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Goals;

public sealed class GetGoalsUseCase
{
    private readonly IGoalRepository _goalRepository;

    public GetGoalsUseCase(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<IReadOnlyCollection<GoalDto>> ExecuteAsync(string userId, CancellationToken cancellationToken = default)
    {
        var items = await _goalRepository.GetByUserAsync(userId, cancellationToken);
        return items
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new GoalDto
            {
                Id = x.Id,
                GoalType = x.GoalType.ToString(),
                TargetValue = x.TargetValue,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsActive = x.IsActive
            })
            .ToArray();
    }
}
