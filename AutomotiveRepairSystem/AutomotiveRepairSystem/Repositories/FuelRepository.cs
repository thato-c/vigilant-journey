using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class FuelRepository:IFuelRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public FuelRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public IQueryable<Fuel> GetAllFuelsAsync()
        {
            return _context.Fuels.AsQueryable();
        }

        public async Task<Fuel?> GetFuelByIdAsync(Guid fuelId)
        {
            return await _context.Fuels
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FuelId == fuelId);
        }

        public void CreateFuel(Fuel fuel)
        {
            _context.Fuels.Add(fuel);
        }

        public async Task<Fuel> DeleteFuel(Guid fuelId)
        {
            var fuel = await _context.Fuels.FindAsync(fuelId);

            if (fuel != null)
            {
                _context.Fuels.Remove(fuel);
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
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
