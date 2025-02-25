using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IMakeRepository
    {
        Task<Make> CreateMakeAsync(Make make);
        Task<Make?> GetMakeByIdAsync(Guid modelId);
        Task<IEnumerable<Make>> GetAllMakesAsync();
        Task<bool> DeleteMakeAsync(Guid makeId);
    }
}
