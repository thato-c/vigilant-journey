using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IServiceRepository
    {
        IQueryable<Service> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        void CreateService(Service service);
        Task<Service> DeleteService(Guid serviceId);
        void Save();
    }
}
