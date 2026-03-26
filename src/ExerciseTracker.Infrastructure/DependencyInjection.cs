using ExerciseTracker.Application.Common.Interfaces;
using ExerciseTracker.Domain.Interfaces;
using ExerciseTracker.Infrastructure.Identity;
using ExerciseTracker.Infrastructure.Persistence;
using ExerciseTracker.Infrastructure.Persistence.Repositories;
using ExerciseTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=exercise-tracker.db";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}
