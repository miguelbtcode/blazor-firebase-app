using Microsoft.AspNetCore.SignalR;
using NetFirebase.Api.Services.Authentication;
using NetFirebase.Api.Services.Products;

namespace NetFirebase.Api;

public class ServerNotifier : BackgroundService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _contextSr;
    private readonly IServiceScopeFactory _scopeFactory;
    private static readonly TimeSpan timeSpan = TimeSpan.FromSeconds(5);
    private readonly ILogger<ServerNotifier> _logger;

    public ServerNotifier(
        IHubContext<NotificationHub, INotificationClient> contextSr,
        IServiceScopeFactory scopeFactory,
        ILogger<ServerNotifier> logger
    )
    {
        _contextSr = contextSr;
        _scopeFactory = scopeFactory;
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

            using var scope = _scopeFactory.CreateAsyncScope();
            var authenticationService =
                scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            var user = await authenticationService.GetUserByEmailAsync(
                "strangerthings@gmail.com",
                stoppingToken
            );

            if (user is not null)
            {
                var products = await productService.GetAllProductsAsync(stoppingToken);
                var random = new Random();
                int randomIndex = random.Next(products.Count());
                var product = products.ElementAt(randomIndex);

                await _contextSr
                    .Clients.User(user.FirebaseId!)
                    .ReceiveNotificationAsync(
                        $"New product available: {product.Name} at ${product.Price}"
                    );
            }
        }
    }
}
