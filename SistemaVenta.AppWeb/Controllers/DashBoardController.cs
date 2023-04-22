using Microsoft.AspNetCore.Mvc;

namespace SistemaVentas.AppWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
