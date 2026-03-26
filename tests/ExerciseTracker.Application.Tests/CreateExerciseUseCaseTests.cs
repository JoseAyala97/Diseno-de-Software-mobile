using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Enums;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.Application.Tests;

public sealed class CreateExerciseUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldStoreExercise_AndReturnSuccessMessage()
    {
        var exerciseRepository = new InMemoryExerciseRepository();
        var notificationRepository = new InMemoryNotificationRepository();
        var useCase = new CreateExerciseUseCase(exerciseRepository, notificationRepository);

        var response = await useCase.ExecuteAsync(new CreateExerciseRequest(
            "user-1",
            ExerciseType.Walking,
            30,
            IntensityLevel.Low,
            DateTime.UtcNow,
            "Morning walk"));

        Assert.Equal("Actividad guardada correctamente.", response.Message);
        Assert.Single(exerciseRepository.Items);
        Assert.Single(notificationRepository.Items);
    }

    private sealed class InMemoryExerciseRepository : IExerciseRepository
    {
        public List<ExerciseRecord> Items { get; } = [];

        public Task AddAsync(ExerciseRecord exerciseRecord, CancellationToken cancellationToken = default)
        {
            Items.Add(exerciseRecord);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<ExerciseRecord>> GetByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyCollection<ExerciseRecord>>(Items.Where(x => x.UserId == userId && x.PerformedOn >= startDate && x.PerformedOn < endDate).ToArray());

        public Task<IReadOnlyCollection<ExerciseRecord>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyCollection<ExerciseRecord>>(Items.Where(x => x.UserId == userId).ToArray());

        public Task<IReadOnlyCollection<ExerciseRecord>> GetRecentByUserAsync(string userId, int take, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyCollection<ExerciseRecord>>(Items.Where(x => x.UserId == userId).Take(take).ToArray());
    }

    private sealed class InMemoryNotificationRepository : INotificationRepository
    {
        public List<Notification> Items { get; } = [];

        public Task AddAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            Items.Add(notification);
            return Task.CompletedTask;
        }

        public Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Task.FromResult(Items.SingleOrDefault(x => x.Id == id));

        public Task<IReadOnlyCollection<Notification>> GetRecentByUserAsync(string userId, int take, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyCollection<Notification>>(Items.Where(x => x.UserId == userId).Take(take).ToArray());

        public Task<int> GetUnreadCountAsync(string userId, CancellationToken cancellationToken = default)
            => Task.FromResult(Items.Count(x => x.UserId == userId && !x.IsRead));

        public Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
