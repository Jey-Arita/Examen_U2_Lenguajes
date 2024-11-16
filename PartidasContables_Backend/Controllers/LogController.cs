using Microsoft.AspNetCore.Mvc;

namespace PartidasContables.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
