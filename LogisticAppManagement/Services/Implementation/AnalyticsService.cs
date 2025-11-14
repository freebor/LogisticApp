using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Interface;

namespace LogisticAppManagement.Services.Implementation
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _uow;

        public AnalyticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DeliveryStats> GetDeliveryAsync()
        {
            var all = await _uow.Deliveries.GetAllAsync();
            return new DeliveryStats
            {
                TotalDeliveries = all.Count(),
                CompletedDeliveries = all.Count(d => d.Status == "Completed"),
                InTransitDeliveries = all.Count(d => d.Status == "InTransit"),
                DelayedDeliveries = all.Count(d => d.Status == "Delayed"),
                pendingDeliveries = all.Count(d => d.Status == "Pending")
            };
        }
    }
}
