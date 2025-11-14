using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Interface;

namespace LogisticAppManagement.Services.Implementation
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _uow;
        public DriverService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Driver> CreateDriverAsync(Driver driver)
        {
            //var driver = new Driver
            //{
            //    Id = Guid.NewGuid(),
            //    Name = dto.Name,
            //    CurrentLat = dto.CurrentLat,
            //    CurrentLng = dto.CurrentLng,
            //    IsAvailable = true,
            //    CreatedAt = DateTime.UtcNow,
            //};

            await _uow.Drivers.AddAsync(driver);
            await _uow.SaveChangesAsync();

            return driver;
        }
        public async Task<IEnumerable<Driver>> GetAllAvailableDriversAsync()
        {
            return await _uow.Drivers.GetAvailableDriversAsync();
        }

        public async Task<Driver?> GetDriverByIdAsync(Guid driverId)
        {
            return await _uow.Drivers.GetByIdAsync(driverId);
        }

        public async Task<IEnumerable<Driver>> FindNearbyDriversAsync(double lat, double lng, double radiusInKm)
        {
            return await _uow.Drivers.GetDriverNearLocationAsync(lat, lng, radiusInKm);
        }

        public async Task<Driver?> FindNearestAvailableDriverAsync(double lat, double lng, double radiusInKm)
        {
            var driver = await _uow.Drivers.GetDriverNearLocationAsync(lat, lng, radiusInKm);

            return driver.FirstOrDefault();
        }

        public async Task UpdateDriverLocationAsync(Guid driverId, double lat, double lng)
        {
            var driver = await _uow.Drivers.GetByIdAsync(driverId);
            if (driver == null)
                throw new Exception("Driver not found");

            driver.CurrentLat = lat;
            driver.CurrentLng = lng;
            driver.UpdatedAt = DateTime.Now;

            _uow.Drivers.UpdateDriverLocation(driver);

            await _uow.SaveChangesAsync();
        }
    }
}
