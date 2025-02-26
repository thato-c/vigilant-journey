using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class MakeRepository:IMakeRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public MakeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Make> GetAllMakesAsync()
        {
            return _context.Makes.AsQueryable();
        }

        public async Task<Make?> GetMakeByIdAsync(Guid makeId)
        {
            return await _context.Makes
                .AsNoTracking().FirstOrDefaultAsync(m => m.MakeId == makeId);
        }

        public void CreateMake(Make make)
        {
            _context.Makes.Add(make);
        }

        public async Task<Make> DeleteMake(Guid makeId)
        {
            var make = await _context.Makes.FindAsync(makeId);

            if (make != null)
            {
                _context.Makes.Remove(make);
            }

            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
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
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        }
    }
}
