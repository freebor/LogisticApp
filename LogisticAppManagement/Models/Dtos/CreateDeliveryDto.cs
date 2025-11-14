namespace LogisticAppManagement.Models.Dtos
{
    public class CreateDeliveryDto
    {
        public string PickupAddress { get; set; } = string.Empty;
        public string DropoffAddress { get; set; } = string.Empty;
        public double PickupLat { get; set; }
        public double PickupLng { get; set; }
        public string? CustomerPhone { get; set; }
        public double DropoffLng { get; set; }
        public double DropoffLat { get; set; }
    }
}
