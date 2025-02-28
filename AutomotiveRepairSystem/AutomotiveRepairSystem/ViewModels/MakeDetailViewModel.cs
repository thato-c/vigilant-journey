using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class MakeDetailViewModel
    {
        public Guid MakeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
