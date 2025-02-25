using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutomotiveRepairSystem.Models
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Display(Name = "Price (excl. VAT)")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceExclVAT {get; set;} = decimal.Zero;

        public ICollection<ScheduledService> ScheduledServices { get; set; }
    }
}
