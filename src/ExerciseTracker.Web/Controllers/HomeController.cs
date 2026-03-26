using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.Web.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Error() => View();
}
