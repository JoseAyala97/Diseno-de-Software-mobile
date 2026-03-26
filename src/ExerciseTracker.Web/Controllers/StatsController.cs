using System.Security.Claims;
using ExerciseTracker.Application.Statistics;
using ExerciseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

[Authorize]
public sealed class StatsController : Controller
{
    private readonly GetExerciseStatisticsUseCase _getExerciseStatisticsUseCase;

    public StatsController(GetExerciseStatisticsUseCase getExerciseStatisticsUseCase)
    {
        _getExerciseStatisticsUseCase = getExerciseStatisticsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var startDate = DateTime.Today.AddDays(-7);
        var endDate = DateTime.Today;
        var summary = await _getExerciseStatisticsUseCase.ExecuteAsync(userId, startDate, endDate, cancellationToken);

        return View(new StatsViewModel
        {
            StartDate = startDate,
            EndDate = endDate,
            Summary = summary
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetSummary(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var summary = await _getExerciseStatisticsUseCase.ExecuteAsync(userId, startDate, endDate, cancellationToken);
        return Json(summary);
    }
}
