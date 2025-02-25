using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service> CreateServiceAsync(Service service);
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<bool> DeleteServiceAsync(Guid serviceId);
    }
}
