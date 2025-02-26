using Microsoft.AspNetCore.Mvc;

namespace AutomotiveRepairSystem.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
