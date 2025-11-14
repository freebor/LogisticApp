using LogisticAppManagement.Data;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;

namespace LogisticAppManagement.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogisticsDbContext _context;

        public IDeliveryRepository Deliveries { get; }
        public IDriverRepository Drivers { get; }
        public IClientRepository Clients { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(LogisticsDbContext context)
        {
            _context = context;
            Deliveries = new DeliveryRepository(context);
            Drivers = new DriverRepository(context);
            Clients = new ClientRepository(context);
            Users = new UserRepository(context);
        }

        
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
