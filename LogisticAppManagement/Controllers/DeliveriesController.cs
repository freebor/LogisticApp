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
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveriesController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryDto dto)
        {
            var delivery = new Delivery
            {
                PickupAddress = dto.PickupAddress,
                DropoffAddress = dto.DropoffAddress,
                PickupLat = dto.PickupLat,
                PickupLng = dto.PickupLng,
                CustomerPhone = dto.CustomerPhone,
                DropoffLng = dto.DropoffLng,
                DropoffLat = dto.DropoffLat,
            };
            var result = await _deliveryService.CreateDeliveryAsync(delivery);

            return Ok(result);
        }

        [HttpPost("{deliveryId}/assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignDriver(Guid deliveryId)
        {
            try
            {
                await _deliveryService.AssignDriverAsync(deliveryId);
                return Ok("Driver assigned successfully");
            }
            catch (Exception)
            {
                return BadRequest("No available driver nearby" );
            }
        }

        [HttpPut("{deliveryId}/status")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> UpdateDeliveryStatus(Guid deliveryId, string status)
        {
            try
            {
                await _deliveryService.UpdateDeliveryStatusAsync(deliveryId, status);
                return Ok("Delivery status updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _deliveryService.GetAllDeliveriesListAsync();
            return Ok(deliveries);
        }
    }
}
