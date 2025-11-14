using LogisticAppManagement.Models.Dtos;

namespace LogisticAppManagement.Services.Interface
{
    public interface IAnalyticsService
    {
        Task<DeliveryStats> GetDeliveryAsync();
    }
}
