namespace AutomotiveRepairSystem.Models
{
    public class ScheduledService
    {
        public Guid ScheduledServiceId { get; set; }

        public bool Complete { get; set; }

        public DateTime ScheduledDate { get; set; } = DateTime.Now;
        
        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }

        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public Guid ServiceBatchId { get; set; }
        public virtual ServiceBatch ServiceBatch { get; set; }
    }
}
