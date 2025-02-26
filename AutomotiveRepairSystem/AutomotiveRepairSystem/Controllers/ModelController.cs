using AutomotiveRepairSystem.Interfaces;
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
    }
}
