using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
        Task<Vehicle?> GetVehicleByIdAsync(Guid vehicleId);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<bool> DeleteVehicleAsync(Guid vehicleId);
    }
}
