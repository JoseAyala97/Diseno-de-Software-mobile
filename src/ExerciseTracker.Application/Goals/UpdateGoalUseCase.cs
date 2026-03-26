using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Goals;

public sealed class UpdateGoalUseCase
{
    private readonly IGoalRepository _goalRepository;

    public UpdateGoalUseCase(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<GoalDto?> ExecuteAsync(UpdateGoalRequest request, CancellationToken cancellationToken = default)
    {
        var goal = await _goalRepository.GetByIdAsync(request.GoalId, cancellationToken);
        if (goal is null)
        {
            return null;
        }

        goal.Update(request.TargetValue, request.StartDate, request.EndDate, request.IsActive);
        await _goalRepository.UpdateAsync(goal, cancellationToken);

        return new GoalDto
        {
            Id = goal.Id,
            GoalType = goal.GoalType.ToString(),
            TargetValue = goal.TargetValue,
            StartDate = goal.StartDate,
            EndDate = goal.EndDate,
            IsActive = goal.IsActive
        };
    }
}
