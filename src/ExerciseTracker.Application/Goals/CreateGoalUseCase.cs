using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Goals;

public sealed class CreateGoalUseCase
{
    private readonly IGoalRepository _goalRepository;

    public CreateGoalUseCase(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<GoalDto> ExecuteAsync(CreateGoalRequest request, CancellationToken cancellationToken = default)
    {
        var goal = new Goal(request.UserId, request.GoalType, request.TargetValue, request.StartDate, request.EndDate);
        await _goalRepository.AddAsync(goal, cancellationToken);

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
