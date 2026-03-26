using ExerciseTracker.Domain.Common;
using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Domain.Entities;

public sealed class Goal : BaseEntity
{
    private Goal()
    {
    }

    public Goal(
        string userId,
        GoalType goalType,
        int targetValue,
        DateTime startDate,
        DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        if (targetValue <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetValue), "Target value must be greater than zero.");
        }

        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be greater than end date.");
        }

        UserId = userId;
        GoalType = goalType;
        TargetValue = targetValue;
        StartDate = startDate.Date;
        EndDate = endDate.Date;
        IsActive = true;
    }

    public string UserId { get; private set; } = string.Empty;
    public GoalType GoalType { get; private set; }
    public int TargetValue { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive { get; private set; }

    public void Update(int targetValue, DateTime startDate, DateTime endDate, bool isActive)
    {
        if (targetValue <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetValue), "Target value must be greater than zero.");
        }

        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be greater than end date.");
        }

        TargetValue = targetValue;
        StartDate = startDate.Date;
        EndDate = endDate.Date;
        IsActive = isActive;
    }
}
