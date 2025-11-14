using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Repository.Interface
{
    public interface IDriverRepository : IGenericRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetAvailableDriversAsync();
        Task<Driver?> GetDriverWithDeliveryAsync(Guid driverId);
        void UpdateDriverLocation(Driver driver);
        Task<IEnumerable<Driver>> GetDriverNearLocationAsync(double lat, double lng, double radiusInKm);
    }
}
