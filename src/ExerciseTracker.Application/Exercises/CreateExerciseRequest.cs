using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Application.Exercises;

public sealed record CreateExerciseRequest(
    string UserId,
    ExerciseType ExerciseType,
    int DurationMinutes,
    IntensityLevel Intensity,
    DateTime PerformedOn,
    string? Notes);
