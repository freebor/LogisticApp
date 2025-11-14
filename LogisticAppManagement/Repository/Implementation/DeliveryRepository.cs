using LogisticAppManagement.Data;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LogisticAppManagement.Repository.Implementation
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(LogisticsDbContext context) : base(context) { }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByDriverIdAsync(Guid driverId)
        {
            return await _dbSet.Where(d => d.DriverId == driverId).Include(d => d.Driver).ToListAsync();
        }

        public async Task<IEnumerable<Delivery>> GetPendingDeliveriesAsync()
        {
            return await _dbSet.Where(d => d.Status == "Pending").Include(d => d.Driver).ToListAsync();
        }
    }
}
