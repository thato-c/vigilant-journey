namespace AutomotiveRepairSystem.Models
{
    public class ServiceBatch
    {
        public Guid BatchId { get; set; }

        public int Complete { get; set; }

        public int To_Be_Completed { get; set; }

        public int Cancelled { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; }
    }
}
