using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Domain.Tests;

public sealed class ExerciseRecordTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenDurationIsInvalid()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ExerciseRecord("user-1", ExerciseType.Running, 0, IntensityLevel.Medium, DateTime.UtcNow, null));
    }
}
