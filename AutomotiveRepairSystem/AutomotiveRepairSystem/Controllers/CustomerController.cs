using AutomotiveRepairSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Controllers
{
    public class CustomerController:Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private ICustomerRepository _customerRepository;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllCustomersAsync().ToListAsync();

            if (customers != null)
            {
                return View(customers);
            }

            return View();
        }
    }
}
