namespace AutomotiveRepairSystem.Models
{
    public class Make
    {
        public Guid MakeId { get; set; }

        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
