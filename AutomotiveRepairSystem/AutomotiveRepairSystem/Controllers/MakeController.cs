using AutomotiveRepairSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Controllers
{
    public class MakeController : Controller
    {
        private readonly ILogger<MakeController> _logger;
        private IMakeRepository _makeRepository;

        public MakeController(ILogger<MakeController> logger, IMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var makes = await _makeRepository.GetAllMakesAsync().ToListAsync();

            if (makes != null)
            {
                return View(makes);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
