using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using AutomotiveRepairSystem.ViewModels;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Map the viewModel to the Customer model
                var customer = new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                };

                // Add and save the new Customer to the database
                _customerRepository.CreateCustomer(customer);
                _customerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                ViewBag.Message = "The Customer was not found.";
                return View();
            }

            var viewModel = new CustomerDetailViewModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CustomerDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customerToDelete = await _customerRepository.GetCustomerByIdAsync(viewModel.CustomerId);

                if (customerToDelete == null)
                {
                    ViewBag.Message = "Customer was not found.";
                    return View();
                }

                await _customerRepository.DeleteCustomerAsync(viewModel.CustomerId);
                await _customerRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
