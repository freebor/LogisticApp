using LogisticAppManagement.Data;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace LogisticAppManagement.Repository.Implementation
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(LogisticsDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
        {
            return await _dbSet.Where(d => d.IsAvailable).ToListAsync();
        }

        public async Task<Driver?> GetDriverWithDeliveryAsync(Guid driverId)
        {
            return await _dbSet.Include(d => d.Deliveries).FirstOrDefaultAsync(d => d.Id == driverId);
        }

        public void UpdateDriverLocation(Driver driver)
        {
            var entry = _context.Entry(driver);
            entry.Property(d => d.CurrentLat).IsModified = true;
            entry.Property(d => d.CurrentLng).IsModified = true;
            entry.Property(d => d.IsAvailable).IsModified = true;
            entry.Property(d => d.UpdatedAt).IsModified = true;
        }

        public async Task<IEnumerable<Driver>> GetDriverNearLocationAsync(double lat, double lng, double radiusInKm)
        {
            var earthRadiusKm = 6371.0;

            var sql = @"SELECT * FROM (
                SELECT *, 
                (@earthRadiuskm * 2 *
                ASIN(SQRT(
                     POWER(SIN(RADIANS((CurrentLat - @lat) / 2)), 2) +
                        COS(RADIANS(@lat)) * COS(RADIANS(CurrentLat)) *
                        POWER(SIN(RADIANS((CurrentLng - @lng) / 2)), 2)
                     ))
                    ) AS DistanceKm
                FROM Drivers
                WHERE IsAvailable = 1)
                AS DistanceTable
                WHERE DistanceKm <= @radiusInKm
                ORDER BY DistanceKm ASC;
            ";

            var drivers = await _context.Drivers.FromSqlRaw(sql, 
                    new SqlParameter("@earthRadiusKm", earthRadiusKm),
                    new SqlParameter("@lat", lat),
                    new SqlParameter("@lng", lng),
                    new SqlParameter("@radiusInKm", radiusInKm))
                .ToListAsync();

            return drivers;
        }
    }
}
