using AutomotiveRepairSystem.Data;
using AutomotiveRepairSystem.Interfaces;
using AutomotiveRepairSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveRepairSystem.Repositories
{
    public class ServiceRepository:IServiceRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Service> GetAllServicesAsync()
        {
            return _context.Services.AsQueryable();
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            return await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        public void CreateService(Service service)
        {
            _context.Services.Add(service);
        }

        public async Task<Service> DeleteService(Guid serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);

            if (service != null)
            {
                _context.Services.Remove(service);
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
