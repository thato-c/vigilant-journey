using AutomotiveRepairSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Controllers
{
    public class FuelController : Controller
    {
        private readonly ILogger<FuelController> _logger;
        private IFuelRepository _fuelRepository;

        public FuelController(ILogger<FuelController> logger, IFuelRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fuels = await _fuelRepository.GetAllFuelsAsync().ToListAsync();

            if (fuels != null)
            {
                return View(fuels);
            }

            return View();
        }
    }
}
