using ExerciseTracker.Application.Dashboard;

namespace ExerciseTracker.Web.ViewModels;

public sealed class DashboardViewModel
{
    public DashboardSummaryDto Summary { get; init; } = new();
}
