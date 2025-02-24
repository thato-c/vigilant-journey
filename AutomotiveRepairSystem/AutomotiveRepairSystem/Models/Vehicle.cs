using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomotiveRepairSystem.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        
        public string LicensePlate { get; set; }

        [Range(1886, 9999)]
        public int Year { get; set; }
        
        public string VIN { get; set; }
        
        public int Mileage { get; set; }

        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("MakeId")]
        public Guid MakeId { get; set; }
        public virtual Make Make { get; set; }

        [ForeignKey("ModelId")]
        public Guid ModelId { get; set; }
        public virtual Model Model { get; set; }
        
        [ForeignKey("FuelId")]
        public Guid FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }
        
        
    }
}
