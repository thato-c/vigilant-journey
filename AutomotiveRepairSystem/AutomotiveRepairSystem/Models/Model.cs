namespace AutomotiveRepairSystem.Models
{
    public class Model
    {
        public Guid ModelId { get; set; }

        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
