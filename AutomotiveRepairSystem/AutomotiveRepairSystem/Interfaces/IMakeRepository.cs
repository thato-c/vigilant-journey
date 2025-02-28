using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IMakeRepository
    {
        IQueryable<Make> GetAllMakesAsync();
        Task<Make?> GetMakeByIdAsync(Guid makeId);
        void CreateMake(Make make);
        Task<Make> DeleteMake(Guid makeId);
        void Save();
        Task SaveAsync();
    }
}
