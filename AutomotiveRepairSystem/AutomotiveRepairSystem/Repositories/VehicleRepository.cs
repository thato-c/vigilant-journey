using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class VehicleRepository:IVehicleRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Vehicle> GetAllVehiclesAsync()
        {
            return _context.Vehicles.AsQueryable();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(Guid vehicleId)
        {
            return await _context.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);
        }

        public void CreateVehicle(Vehicle vehicle)
        {
           _context.Vehicles.Add(vehicle);
        }

        public async Task<Vehicle> DeleteVehicle(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            return null;
        }

        public void Save()
        {
            _context?.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _context?.Dispose();
                    }
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
