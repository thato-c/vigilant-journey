using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId);
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    }
}
