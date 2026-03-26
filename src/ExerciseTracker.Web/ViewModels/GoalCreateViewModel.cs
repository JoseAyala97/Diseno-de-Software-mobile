using System.ComponentModel.DataAnnotations;
using ExerciseTracker.Application.Goals;
using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Web.ViewModels;

public sealed class GoalCreateViewModel
{
    [Required]
    [Display(Name = "Tipo de meta")]
    public GoalType GoalType { get; set; }

    [Required]
    [Range(1, 10000)]
    [Display(Name = "Valor objetivo")]
    public int TargetValue { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha inicio")]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha fin")]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);

    public IReadOnlyCollection<GoalDto> Goals { get; init; } = [];
}
