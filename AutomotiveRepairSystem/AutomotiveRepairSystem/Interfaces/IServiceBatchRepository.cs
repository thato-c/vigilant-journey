using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IServiceBatchRepository
    {
        Task<ServiceBatch?> GetServiceBatchByIdAsync(Guid serviceBatchId);
        Task<IEnumerable<Service>> GetAllServiceBatchesAsync();
    }
}
