using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Interface;

namespace LogisticAppManagement.Services.Implementation
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDriverService _driver;
        public DeliveryService(IUnitOfWork uow, IDriverService driver)
        {
            _uow = uow;
            _driver = driver;
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
        {
            await _uow.Deliveries.AddAsync(delivery);
            await _uow.SaveChangesAsync();

            return delivery;
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesListAsync()
        {
            return await _uow.Deliveries.GetAllAsync();
        }

        public async Task<IEnumerable<Delivery>> GetPendingDeliveriesListAsync() => 
            await _uow.Deliveries.GetPendingDeliveriesAsync();

        public async Task AssignDriverAsync(Guid deliverId)
        {
            var delivery = await _uow.Deliveries.GetByIdAsync(deliverId);
            if (delivery == null)
                throw new Exception("Delivery not found");

            var nearestDriver = await _driver.FindNearestAvailableDriverAsync(delivery.PickupLat, delivery.PickupLng, 10);
            if (nearestDriver == null)
                throw new Exception("No available driver nearby");

            delivery.DriverId = nearestDriver.Id;
            delivery.Status = "Assigned";

            nearestDriver.IsAvailable = false;

            _uow.Deliveries.Update(delivery);
            _uow.Drivers.Update(nearestDriver);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateDeliveryStatusAsync(Guid deliveryId, string status)
        {
            var delivery = await _uow.Deliveries.GetByIdAsync(deliveryId);
            if (delivery == null)
                throw new Exception("Delivery not found");

            delivery.Status = status;
            delivery.UpdatedAt = DateTime.UtcNow;

            _uow.Deliveries.Update(delivery);
            await _uow.SaveChangesAsync();
        }
    }
}
