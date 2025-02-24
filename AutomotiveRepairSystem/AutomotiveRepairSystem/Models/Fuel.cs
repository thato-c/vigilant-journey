namespace AutomotiveRepairSystem.Models
{
    public class Fuel
    {
        public Guid FuelId { get; set; }

        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
