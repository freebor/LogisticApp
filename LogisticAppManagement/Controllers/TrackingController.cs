using LogisticAppManagement.Hubs;
using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LogisticAppManagement.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class TrackingController : ControllerBase
    {
        private readonly IHubContext<TrackingHub> _hubContext;
        private readonly IDriverService _driverService;
        public TrackingController(IHubContext<TrackingHub> hubContext, IDriverService driverService)
        {
            _hubContext = hubContext;
            _driverService = driverService;
        }

        [HttpPost("{driverId}/location")]
        public async Task<IActionResult> BroadcastLocation(Guid driverId, double lat, double lng)
        {
            try
            {
                await _driverService.UpdateDriverLocationAsync(driverId, lat, lng);

            }
            catch
            {
                return BadRequest("Unable to update location.");
            }

            await _hubContext.Clients.All.SendAsync("DriverLocationUpdated", new DriverLocationDto
            {
                DriverId = driverId,
                Latitude = lat,
                Longitude = lng,
                UpdatedAt = DateTime.UtcNow
            });

            return Ok("Location Broadcasted");
        }
    }
}
