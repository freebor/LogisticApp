namespace LogisticAppManagement.Services.Interface
{
    public interface INotificationService
    {
        Task SendDeliveryAssignedNotificationAsync(Guid driverId, Guid deliveryId);
        Task SendStatusUpdateNotificationAsync(Guid userId, string status);
    }
}
