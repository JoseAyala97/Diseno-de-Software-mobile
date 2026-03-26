using System.ComponentModel.DataAnnotations;
using ExerciseTracker.Application.Exercises;
using ExerciseTracker.Domain.Enums;

namespace ExerciseTracker.Web.ViewModels;

public sealed class ExerciseCreateViewModel
{
    [Required]
    [Display(Name = "Tipo de ejercicio")]
    public ExerciseType ExerciseType { get; set; }

    [Required]
    [Range(1, 600)]
    [Display(Name = "Duracion en minutos")]
    public int DurationMinutes { get; set; }

    [Required]
    [Display(Name = "Intensidad")]
    public IntensityLevel Intensity { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime PerformedOn { get; set; } = DateTime.Today;

    [Display(Name = "Comentarios")]
    [StringLength(400)]
    public string? Notes { get; set; }

    public IReadOnlyCollection<ExerciseHistoryItemDto> History { get; init; } = [];
}
