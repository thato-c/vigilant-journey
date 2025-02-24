namespace AutomotiveRepairSystem.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
