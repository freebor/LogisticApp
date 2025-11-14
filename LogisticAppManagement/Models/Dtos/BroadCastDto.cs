namespace LogisticAppManagement.Models.Dtos
{
    public class BroadCastDto
    {
        public Guid driverId = Guid.Empty;
        public double lat { set; get; } 
        public double lng { set; get; }
    }
}