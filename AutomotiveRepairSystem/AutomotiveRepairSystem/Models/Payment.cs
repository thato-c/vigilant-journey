using System.ComponentModel.DataAnnotations.Schema;

namespace AutomotiveRepairSystem.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }

        public Guid InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
