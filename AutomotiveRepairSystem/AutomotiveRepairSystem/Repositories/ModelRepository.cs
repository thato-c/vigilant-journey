using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class ModelRepository:IModelRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public ModelRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public IQueryable<Model> GetAllModelsAsync()
        {
            return _context.Models.AsQueryable();
        }

        public async Task<Model?> GetModelByIdAsync(Guid modelId)
        {
            return await _context.Models
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ModelId == modelId);
        }

        public void CreateModel(Model model)
        {
            _context.Models.Add(model);
        }

        public async Task<Model> DeleteModel(Guid modelId)
        {
            var model = await _context.Models.FindAsync(modelId);

            if (model != null)
            {
                _context.Models.Remove(model);
            }

            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
