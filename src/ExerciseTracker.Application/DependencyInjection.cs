using ExerciseTracker.Application.Authentication;
using ExerciseTracker.Application.Dashboard;
using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Application.Goals;
using ExerciseTracker.Application.Notifications;
using ExerciseTracker.Application.Statistics;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        services.AddScoped<LogoutUserUseCase>();
        services.AddScoped<GetDashboardSummaryUseCase>();
        services.AddScoped<CreateExerciseUseCase>();
        services.AddScoped<GetExerciseHistoryUseCase>();
        services.AddScoped<CreateGoalUseCase>();
        services.AddScoped<UpdateGoalUseCase>();
        services.AddScoped<GetGoalsUseCase>();
        services.AddScoped<GetExerciseStatisticsUseCase>();
        services.AddScoped<CreateReminderUseCase>();
        services.AddScoped<GetUserNotificationsUseCase>();
        services.AddScoped<MarkNotificationAsReadUseCase>();
        return services;
    }
}
