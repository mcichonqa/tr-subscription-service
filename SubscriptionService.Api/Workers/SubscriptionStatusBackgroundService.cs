using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubscriptionService.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SubscriptionService.Api.Workers
{
    public class SubscriptionStatusBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SubscriptionStatusBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var _subscriptionService = _serviceScopeFactory.CreateScope()
                    .ServiceProvider.GetRequiredService<ISubscriptionService>();

                await _subscriptionService.SetSubscriptionStatusAsync();
                Thread.Sleep(30000);
            }
        }
    }
}
