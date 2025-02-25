using System.ComponentModel.DataAnnotations.Schema;

namespace AutomotiveRepairSystem.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }

        public DateTime CreationDate { get; set; }
        
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountDue { get; set; }

        public string Status { get; set; }

        public Guid ServiceBatchId { get; set; }
        public virtual ServiceBatch ServiceBatch { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
