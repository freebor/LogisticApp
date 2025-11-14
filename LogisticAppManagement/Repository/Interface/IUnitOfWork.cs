using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IDeliveryRepository Deliveries { get; }
        IDriverRepository Drivers { get; }
        IClientRepository Clients { get; }
        IUserRepository Users{ get; }
        Task<int> SaveChangesAsync();
    }
}
