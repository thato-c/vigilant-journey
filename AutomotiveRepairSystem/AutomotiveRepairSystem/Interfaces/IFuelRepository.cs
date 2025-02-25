using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IFuelRepository
    {
        Task<Fuel> CreateFuelAsync(Fuel fuel);
        Task<Fuel?> GetFuelByIdAsync(Guid fuelId);
        Task<IEnumerable<Fuel>> GetAllFuelsAsync();
        Task<bool> DeleteFuelAsync(Guid fuelId);
    }
}
