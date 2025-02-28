using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(Guid customerId);
        void CreateCustomer(Customer customer);
        Task<Customer> DeleteCustomerAsync(Guid customerId);
        void Save();
        Task SaveAsync();
    }
}
