namespace EventBooking.Notification.Hubs;


public interface INotificationClient
{
    Task ReceiveNotification(string title, string message);
}