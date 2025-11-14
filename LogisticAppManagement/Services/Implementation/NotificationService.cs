using LogisticAppManagement.Hubs;
using LogisticAppManagement.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace LogisticAppManagement.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<TrackingHub> _hubContext;

        public NotificationService(IHubContext<TrackingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendDeliveryAssignedNotificationAsync(Guid driverId, Guid deliveryId)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveDeliveryAssigned", new
            {
                DriverId = driverId,
                DeliveryId = deliveryId,
                message = "A driver has been assigned to your delivery"
            });
        }

        public async Task SendStatusUpdateNotificationAsync(Guid userId, string status) 
        {
            await _hubContext.Clients.All.SendAsync("DeliveryStatusUpdated", new
            {
                UserId = userId,
                Status = status,
            });
        }
    }
}
