namespace LogisticAppManagement.Models.Entities
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public double CurrentLat { get; set; }
        public double CurrentLng { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Delivery>? Deliveries { get; set; }
    }
}