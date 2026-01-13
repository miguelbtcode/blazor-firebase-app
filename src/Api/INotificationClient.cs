namespace NetFirebase.Api;

public interface INotificationClient
{
    Task ReceiveNotificationAsync(string message);
}
