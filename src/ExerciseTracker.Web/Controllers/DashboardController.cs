using System.Security.Claims;
using ExerciseTracker.Application.Dashboard;
using ExerciseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

[Authorize]
public sealed class DashboardController : Controller
{
    private readonly GetDashboardSummaryUseCase _getDashboardSummaryUseCase;

    public DashboardController(GetDashboardSummaryUseCase getDashboardSummaryUseCase)
    {
        _getDashboardSummaryUseCase = getDashboardSummaryUseCase;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userName = User.Identity?.Name ?? "Usuario";
        var summary = await _getDashboardSummaryUseCase.ExecuteAsync(userId, userName, cancellationToken);

        return View(new DashboardViewModel
        {
            Summary = summary
        });
    }
}
