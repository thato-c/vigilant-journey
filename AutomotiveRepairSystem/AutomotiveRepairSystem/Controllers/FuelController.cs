using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using AutomotiveRepairSystem.ViewModels;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FuelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the viewModel to the Fuel model
                var fuel = new Fuel
                {
                    Name = viewModel.Name,
                };

                // Add and save the new fuel to the database
                _fuelRepository.CreateFuel(fuel);
                _fuelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid fuelId)
        {
            var fuel = await _fuelRepository.GetFuelByIdAsync(fuelId);

            if (fuel == null)
            {
                ViewBag.Message = "The Fuel has not been found.";
                return View();
            }

            var viewModel = new FuelDetailViewModel
            {
                FuelId = fuel.FuelId,
                Name = fuel.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(FuelDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var fuelToDelete = await _fuelRepository.GetFuelByIdAsync(viewModel.FuelId);

                if  (fuelToDelete == null)
                {
                    ViewBag.Message = "Fuel was not found.";
                    return View();
                }

                await _fuelRepository.DeleteFuel(viewModel.FuelId);
                await _fuelRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
