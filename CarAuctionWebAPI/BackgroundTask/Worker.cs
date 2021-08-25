using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarAuctionWebAPI.BackgroundTask
{
    public class Worker: IWorker
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    CarAuctionContext carAuctionContext =
                        scope.ServiceProvider.GetRequiredService<CarAuctionContext>();
                    var lots = carAuctionContext.Lots.Where(ld=>ld.EndDate <= DateTime.Now && ld.Status.Equals(Status.Approved)).ToList();
                    foreach (var l in lots)
                    {
                        
                        var bids = carAuctionContext.Bids.Where(i =>
                            i.LotId.Equals(l.Id) && i.BidStatus.Equals(BidStatus.Active)).ToList();
                        foreach (var b in bids)
                        {
                            b.BidStatus = BidStatus.Won;
                        }

                        l.Status = Status.Denied;
                    }
                    carAuctionContext.SaveChanges();
                    await Task.Delay(1000, cancellationToken);

                }
                
            }
        }
    }
}
