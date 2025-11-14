using Microsoft.AspNetCore.SignalR;

namespace LogisticAppManagement.Hubs
{
    public class TrackingHub : Hub
    {
        //when a client (driver or customer) connects to the hub
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveConnectionId", "You are connected to the tracking hub");
            await base.OnConnectedAsync();
        }

        //when updating driver location from mobile apps or GPS devices
        public async Task UpdateDriverLocation(Guid driverId, double lat, double lng)
        {
            //broadcast the updated location to all connected clients
            await Clients.All.SendAsync("ReceiveDriverLocation", new
            {
                DriverId = driverId,
                Latitude = lat,
                Longitude = lng,
                UpdatedAt = DateTime.UtcNow
            });
        }

        //when delivery status changes
        public async Task UpdateDeliveryStatus(Guid deliveryId, string status)
        {
            //broadcast the updated delivery status to all connected clients
            await Clients.All.SendAsync("ReceiveDeliveryStatus", new
            {
                DeliveryId = deliveryId,
                Status = status,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
