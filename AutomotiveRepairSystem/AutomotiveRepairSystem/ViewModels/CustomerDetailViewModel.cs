using System.ComponentModel.DataAnnotations;

namespace AutomotiveRepairSystem.ViewModels
{
    public class CustomerDetailViewModel
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is in valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string Phone { get; set; }
    }
}
