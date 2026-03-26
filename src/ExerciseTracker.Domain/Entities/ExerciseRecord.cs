using ExerciseTracker.Domain.Common;
using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Domain.Entities;

public sealed class ExerciseRecord : BaseEntity
{
    private ExerciseRecord()
    {
    }

    public ExerciseRecord(
        string userId,
        ExerciseType exerciseType,
        int durationMinutes,
        IntensityLevel intensity,
        DateTime performedOn,
        string? notes)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        if (durationMinutes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(durationMinutes), "Duration must be greater than zero.");
        }

        UserId = userId;
        ExerciseType = exerciseType;
        DurationMinutes = durationMinutes;
        Intensity = intensity;
        PerformedOn = performedOn;
        Notes = notes?.Trim();
    }

    public string UserId { get; private set; } = string.Empty;
    public ExerciseType ExerciseType { get; private set; }
    public int DurationMinutes { get; private set; }
    public IntensityLevel Intensity { get; private set; }
    public DateTime PerformedOn { get; private set; }
    public string? Notes { get; private set; }
}
