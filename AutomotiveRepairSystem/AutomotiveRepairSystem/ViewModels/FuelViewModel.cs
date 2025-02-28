using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class FuelViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
