using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using AutomotiveRepairSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private IServiceRepository _serviceRepository;

        public ServiceController(ILogger<ServiceController> logger, IServiceRepository serviceRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllServicesAsync().ToListAsync();

            if (services != null)
            {
                return View(services);
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
        public IActionResult Create(ServiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the viewModel to the Service Model
                var service = new Service
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    PriceExclVAT = viewModel.PriceExclVAT,
                };

                // Add and save the new service to the database
                _serviceRepository.CreateService(service);
                _serviceRepository.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid serviceId)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);

            if (service == null)
            {
                ViewBag.Message = "The Service has not been found";
                return View();
            }

            var viewModel = new ServiceDetailViewModel
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                Description = service.Description,
                PriceExclVAT = service.PriceExclVAT,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ServiceDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var serviceToDelete = await _serviceRepository.GetServiceByIdAsync(viewModel.ServiceId);

                if  (serviceToDelete == null)
                {
                    ViewBag.Message = "Service was not found.";
                    return View();
                }

                await _serviceRepository.DeleteService(viewModel.ServiceId);
                await _serviceRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
