using Microsoft.AspNetCore.SignalR;

namespace NetFirebase.Api;

public class ServerNotifier : BackgroundService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _contextSr;
    private static readonly TimeSpan timeSpan = TimeSpan.FromSeconds(5);
    private readonly ILogger<ServerNotifier> _logger;

    public ServerNotifier(
        IHubContext<NotificationHub, INotificationClient> contextSr,
        ILogger<ServerNotifier> logger
    )
    {
        _contextSr = contextSr;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(timeSpan);
        while (
            !stoppingToken.IsCancellationRequested
            && await timer.WaitForNextTickAsync(stoppingToken)
        )
        {
            var datetime = DateTime.Now;
            _logger.LogInformation(
                "Ejecutando {ServiceName} {DateTime}",
                nameof(ServerNotifier),
                datetime
            );
            await _contextSr.Clients.All.ReceiveNotificationAsync($"Server time = {datetime}");
        }
    }
}
