namespace LogisticAppManagement.Models.Dtos
{
    public class DeliveryStats
    {
        public int TotalDeliveries { get; set; }
        public int CompletedDeliveries { get; set; }
        public int InTransitDeliveries { get; set; }
        public int DelayedDeliveries { get; set; }
        public int pendingDeliveries { get; set; }
    }
}