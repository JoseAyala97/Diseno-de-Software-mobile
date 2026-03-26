namespace ExerciseTracker.Application.Common.Interfaces;

public sealed record AuthResult(bool Succeeded, IEnumerable<string> Errors, string? UserId = null);
