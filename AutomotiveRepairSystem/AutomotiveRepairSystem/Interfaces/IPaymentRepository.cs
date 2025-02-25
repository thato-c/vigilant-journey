using AutomotiveRepairSystem.Models;

namespace AutomotiveRepairSystem.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetPaymentByIdAsync(Guid paymentId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    }
}
