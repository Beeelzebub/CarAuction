using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarAuctionWebAPI.BackgroundTask
{
    public class DerivedBackgroundPrinter: BackgroundService
    {
        private readonly ILogger _logger; 
        private readonly IServiceProvider _serviceProvider;

        public DerivedBackgroundPrinter(ILogger<DerivedBackgroundPrinter> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Background service is running.");

            await DoWork(stoppingToken);
        }
        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service is running.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IWorker worker =
                    scope.ServiceProvider.GetRequiredService<IWorker>();

                await worker.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
