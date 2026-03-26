using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Exercises;

public sealed class CreateExerciseUseCase
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly INotificationRepository _notificationRepository;

    public CreateExerciseUseCase(
        IExerciseRepository exerciseRepository,
        INotificationRepository notificationRepository)
    {
        _exerciseRepository = exerciseRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<CreateExerciseResponse> ExecuteAsync(CreateExerciseRequest request, CancellationToken cancellationToken = default)
    {
        var record = new ExerciseRecord(
            request.UserId,
            request.ExerciseType,
            request.DurationMinutes,
            request.Intensity,
            request.PerformedOn,
            request.Notes);

        await _exerciseRepository.AddAsync(record, cancellationToken);
        await _notificationRepository.AddAsync(
            new Notification(request.UserId, "Actividad registrada", "Tu actividad fue guardada correctamente."),
            cancellationToken);

        return new CreateExerciseResponse(record.Id, "Actividad guardada correctamente.");
    }
}
