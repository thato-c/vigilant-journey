using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class ModelViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
