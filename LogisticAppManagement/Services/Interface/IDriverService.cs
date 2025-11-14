using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Services.Interface
{
    public interface IDriverService
    {
        Task<Driver> CreateDriverAsync(Driver driver);
        Task<IEnumerable<Driver>> GetAllAvailableDriversAsync();
        Task<Driver?> GetDriverByIdAsync(Guid driverId);
        Task<IEnumerable<Driver>> FindNearbyDriversAsync(double lat, double lng, double radiusInKm);
        Task<Driver?> FindNearestAvailableDriverAsync(double lat, double lng, double radiusInKm);
        Task UpdateDriverLocationAsync(Guid driverId, double lat, double lng);
    }
}
