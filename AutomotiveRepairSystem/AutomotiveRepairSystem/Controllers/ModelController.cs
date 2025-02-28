using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using AutomotiveRepairSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Controllers
{
    public class ModelController : Controller
    {
        private readonly ILogger<ModelController> _logger;
        private IModelRepository _modelRepository;

        public ModelController(ILogger<ModelController> logger, IModelRepository modelRepository)
        {
            _logger = logger;
            _modelRepository = modelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _modelRepository.GetAllModelsAsync().ToListAsync();

            if (models != null)
            {
                return View(models);
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
        public IActionResult Create(ModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the viewModel to the Model model
                var model = new Model
                {
                    Name = viewModel.Name,
                };

                // Add and save the new model to the database
                _modelRepository.CreateModel(model);
                _modelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid modelId)
        {
            var model = await _modelRepository.GetModelByIdAsync(modelId);

            if (model == null)
            {
                ViewBag.Message = "The Model has not been found.";
                return View();
            }

            var viewModel = new ModelDetailViewModel
            {
                ModelId = model.ModelId,
                Name = model.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ModelDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var modelToDelete = await _modelRepository.GetModelByIdAsync(viewModel.ModelId);

                if (modelToDelete == null)
                {
                    ViewBag.Message = "Model was not found.";
                    return View();
                }

                await _modelRepository.DeleteModel(viewModel.ModelId);
                await _modelRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
