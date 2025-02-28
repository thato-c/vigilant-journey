using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class FuelDetailViewModel
    {
        public Guid FuelId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
