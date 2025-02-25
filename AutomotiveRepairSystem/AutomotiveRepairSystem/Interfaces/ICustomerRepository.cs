using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> DeleteCustomerAsync(Guid customerId);
    }
}
