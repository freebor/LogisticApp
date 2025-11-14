namespace LogisticAppManagement.Models.Dtos
{
    internal class DriverLocationDto
    {
        public Guid DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}