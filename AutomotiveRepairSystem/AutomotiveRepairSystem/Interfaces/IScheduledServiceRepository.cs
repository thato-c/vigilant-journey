using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IScheduledServiceRepository
    {
        Task<ScheduledService> CreateScheduledServiceAsync(ScheduledService scheduledService);
        Task<ScheduledService?> GetScheduledServiceByIdAsync(Guid scheduledSserviceId);
        Task<IEnumerable<Service>> GetAllScheduledServicesAsync();
    }
}
