using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using AutomotiveRepairSystem.ViewModels;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MakeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the viewModel to the Make model
                var make = new Make
                {
                    Name = viewModel.Name
                };

                // Add and save the new make to the database
                _makeRepository.CreateMake(make);
                _makeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
