using System.ComponentModel.DataAnnotations;
using ExerciseTracker.Application.Notifications;

namespace ExerciseTracker.Web.ViewModels;

public sealed class NotificationViewModel
{
    public IReadOnlyCollection<ReminderDto> Reminders { get; init; } = [];
    public IReadOnlyCollection<NotificationDto> Notifications { get; init; } = [];

    [Required]
    [Display(Name = "Titulo")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Mensaje")]
    public string Message { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Hora")]
    public TimeOnly ReminderTime { get; set; } = new(8, 0);
}
