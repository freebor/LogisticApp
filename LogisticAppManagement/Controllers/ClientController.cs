using LogisticAppManagement.Models.Dtos;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsAppManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterClient([FromBody] CreateClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Client name is required.");

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.PhoneNumber
            };

            var result = await _clientService.RegisterClientAsync(client);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
                return NotFound("Client not found.");

            return Ok(client);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }
    }
}
