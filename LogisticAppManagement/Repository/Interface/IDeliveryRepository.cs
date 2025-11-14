using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Repository.Interface
{
    public interface IDeliveryRepository : IGenericRepository<Delivery>
    {
        Task<IEnumerable<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId);
        Task<IEnumerable<Delivery>> GetPendingDeliveriesAsync();
    }
}
