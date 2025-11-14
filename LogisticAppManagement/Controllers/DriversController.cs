using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticAppManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDriver([FromBody] CreateDriverDto dto)
        {
            var driver = new Driver
            {
                Name = dto.Name,
                CurrentLat = dto.CurrentLat,
                CurrentLng = dto.CurrentLng,
                IsAvailable = true
            };
            var result = await _driverService.CreateDriverAsync(driver);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailableDrivers()
        {
            var drivers = await _driverService.GetAllAvailableDriversAsync();
            return Ok(drivers);
        }

        [HttpPut("{driverId}/location")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> UpdateDriverLocation(Guid driverId, [FromQuery] double lat, [FromQuery] double lng)
        {
            try
            {
                await _driverService.UpdateDriverLocationAsync(driverId, lat, lng);
                return Ok("Driver location updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
