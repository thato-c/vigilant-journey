using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IModelRepository
    {
        Task<Model> CreateModelAsync(Model model);
        Task<Model?> GetModelByIdAsync(Guid modelId);
        Task<IEnumerable<Model>> GetAllModelsAsync();
        Task<bool> DeleteModelAsync(Guid modelId);
    }
}
