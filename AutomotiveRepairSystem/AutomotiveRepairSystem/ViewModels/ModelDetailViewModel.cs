using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class ModelDetailViewModel
    {
        public Guid ModelId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
