namespace ExerciseTracker.Application.Authentication;

public sealed record LoginUserRequest(string Email, string Password, bool RememberMe);
