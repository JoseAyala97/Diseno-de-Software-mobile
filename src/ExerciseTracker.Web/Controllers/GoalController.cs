using System.Security.Claims;
using ExerciseTracker.Application.Goals;
using ExerciseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

[Authorize]
public sealed class GoalController : Controller
{
    private readonly CreateGoalUseCase _createGoalUseCase;
    private readonly GetGoalsUseCase _getGoalsUseCase;

    public GoalController(CreateGoalUseCase createGoalUseCase, GetGoalsUseCase getGoalsUseCase)
    {
        _createGoalUseCase = createGoalUseCase;
        _getGoalsUseCase = getGoalsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var goals = await _getGoalsUseCase.ExecuteAsync(userId, cancellationToken);
        return View(new GoalCreateViewModel { Goals = goals });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GoalCreateViewModel model, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (!ModelState.IsValid)
        {
            var goals = await _getGoalsUseCase.ExecuteAsync(userId, cancellationToken);
            return View("Index", new GoalCreateViewModel
            {
                GoalType = model.GoalType,
                TargetValue = model.TargetValue,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Goals = goals
            });
        }

        var goal = await _createGoalUseCase.ExecuteAsync(new CreateGoalRequest(
            userId,
            model.GoalType,
            model.TargetValue,
            model.StartDate,
            model.EndDate), cancellationToken);

        return Json(new { success = true, message = "Meta guardada correctamente.", data = goal });
    }
}
