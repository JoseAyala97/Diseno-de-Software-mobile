using System.Security.Claims;
using ExerciseTracker.Application.Notifications;
using ExerciseTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

[Authorize]
public sealed class NotificationController : Controller
{
    private readonly CreateReminderUseCase _createReminderUseCase;
    private readonly GetUserNotificationsUseCase _getUserNotificationsUseCase;
    private readonly MarkNotificationAsReadUseCase _markNotificationAsReadUseCase;

    public NotificationController(
        CreateReminderUseCase createReminderUseCase,
        GetUserNotificationsUseCase getUserNotificationsUseCase,
        MarkNotificationAsReadUseCase markNotificationAsReadUseCase)
    {
        _createReminderUseCase = createReminderUseCase;
        _getUserNotificationsUseCase = getUserNotificationsUseCase;
        _markNotificationAsReadUseCase = markNotificationAsReadUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var data = await _getUserNotificationsUseCase.ExecuteAsync(userId, cancellationToken);
        return View(new NotificationViewModel
        {
            Reminders = data.Reminders,
            Notifications = data.Notifications
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateReminder(NotificationViewModel model, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Datos de recordatorio invalidos." });
        }

        var reminder = await _createReminderUseCase.ExecuteAsync(
            new CreateReminderRequest(userId, model.Title, model.Message, model.ReminderTime),
            cancellationToken);

        return Json(new { success = true, message = "Recordatorio creado correctamente.", data = reminder });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken cancellationToken)
    {
        var updated = await _markNotificationAsReadUseCase.ExecuteAsync(id, cancellationToken);
        return Json(new { success = updated });
    }
}
