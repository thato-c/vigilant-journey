using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class CustomerRepository:ICustomerRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAllCustomersAsync()
        {
            return _context.Customers.AsQueryable();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<Customer> DeleteCustomer(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
