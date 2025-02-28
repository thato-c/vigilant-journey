using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomotiveRepairSystem.ViewModels
{
    public class ServiceViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price (excl. VAT)")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceExclVAT { get; set; }
    }
}
