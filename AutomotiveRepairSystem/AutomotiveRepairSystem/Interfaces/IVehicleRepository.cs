using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> GetAllVehiclesAsync();
        Task<Vehicle?> GetVehicleByIdAsync(Guid vehicleId);
        void CreateVehicle(Vehicle vehicle);
        Task<Vehicle> DeleteVehicle(Guid vehicleId);
        void Save();
        Task SaveAsync();
    }
}
