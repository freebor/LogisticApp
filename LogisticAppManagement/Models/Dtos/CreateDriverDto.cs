namespace LogisticAppManagement.Models.Dtos
{
    public class CreateDriverDto
    {
        public string Name { get; set; } = string.Empty;
        public double CurrentLat { get; set; }
        public double CurrentLng { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}