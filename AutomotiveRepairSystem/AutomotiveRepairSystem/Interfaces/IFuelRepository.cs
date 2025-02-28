using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IFuelRepository
    {
        IQueryable<Fuel> GetAllFuelsAsync();
        Task<Fuel?> GetFuelByIdAsync(Guid fuelId);
        void CreateFuel(Fuel fuel);
        Task<Fuel> DeleteFuel(Guid fuelId);
        void Save();
        Task SaveAsync();
    }
}
