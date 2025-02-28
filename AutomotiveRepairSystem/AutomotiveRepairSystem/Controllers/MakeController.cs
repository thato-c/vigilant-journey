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
        public IActionResult Create(MakeViewModel viewModel)
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid makeId)
        {
            var make = await _makeRepository.GetMakeByIdAsync(makeId);

            if (make == null)
            {
                ViewBag.Message = "The Model has not been found.";
                return View();
            }

            var viewModel = new MakeDetailViewModel
            {
                MakeId = make.MakeId,
                Name = make.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MakeDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var makeToDelete = await _makeRepository.GetMakeByIdAsync(viewModel.MakeId);

                if (makeToDelete == null)
                {
                    ViewBag.Message = "Make was not found.";
                    return View();
                }

                await _makeRepository.DeleteMake(viewModel.MakeId);
                await _makeRepository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
