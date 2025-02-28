using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IModelRepository
    {
        IQueryable<Model> GetAllModelsAsync();
        Task<Model?> GetModelByIdAsync(Guid modelId);
        void CreateModel(Model model);
        Task<Model> DeleteModel(Guid modelId);
        void Save();
        Task SaveAsync();
    }
}
