using System.Security.Claims;
using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

[Authorize]
public sealed class ExerciseController : Controller
{
    private readonly CreateExerciseUseCase _createExerciseUseCase;
    private readonly GetExerciseHistoryUseCase _getExerciseHistoryUseCase;

    public ExerciseController(CreateExerciseUseCase createExerciseUseCase, GetExerciseHistoryUseCase getExerciseHistoryUseCase)
    {
        _createExerciseUseCase = createExerciseUseCase;
        _getExerciseHistoryUseCase = getExerciseHistoryUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var history = await _getExerciseHistoryUseCase.ExecuteAsync(userId, cancellationToken);
        return View(new ExerciseCreateViewModel { History = history });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExerciseCreateViewModel model, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (!ModelState.IsValid)
        {
            var history = await _getExerciseHistoryUseCase.ExecuteAsync(userId, cancellationToken);
            return View("Index", new ExerciseCreateViewModel
            {
                ExerciseType = model.ExerciseType,
                DurationMinutes = model.DurationMinutes,
                Intensity = model.Intensity,
                PerformedOn = model.PerformedOn,
                Notes = model.Notes,
                History = history
            });
        }

        var response = await _createExerciseUseCase.ExecuteAsync(new CreateExerciseRequest(
            userId,
            model.ExerciseType,
            model.DurationMinutes,
            model.Intensity,
            model.PerformedOn,
            model.Notes), cancellationToken);

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, message = response.Message });
        }

        TempData["SuccessMessage"] = response.Message;
        return RedirectToAction(nameof(Index));
    }
}
