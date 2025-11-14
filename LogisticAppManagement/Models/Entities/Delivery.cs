namespace LogisticAppManagement.Models.Entities
{
    public class Delivery : BaseEntity
    {
        public string PickupAddress { get; set; } = string.Empty;
        public string DropoffAddress { get; set; } = string.Empty;
        public double PickupLat { get; set; }
        public double PickupLng { get; set; }
        public double DropoffLng { get; set; }
        public double DropoffLat { get; set; }
        public string? CustomerPhone { get; set; }
        public string Status { get; set; } = "Pending";
        public Guid DriverId { get; set; }

        public Driver? Driver { get; set; }
    }
}
