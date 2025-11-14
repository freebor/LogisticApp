using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Services.Interface
{
    public interface IDeliveryService
    {
        Task<Delivery> CreateDeliveryAsync(Delivery delivery);
        Task<IEnumerable<Delivery>> GetAllDeliveriesListAsync();
        Task<IEnumerable<Delivery>> GetPendingDeliveriesListAsync();
        Task AssignDriverAsync(Guid deliverId);
        Task UpdateDeliveryStatusAsync(Guid deliveryId, string status);
    }
}
