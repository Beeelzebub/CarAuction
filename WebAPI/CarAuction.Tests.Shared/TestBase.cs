using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.Tests.Shared.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Tests.Shared
{
    public abstract class TestBase 
    {
        protected readonly TestCarAuctionDbContext _testDbContext;

        protected TestBase()
        {
            _testDbContext = GetTestCarAuctionDbContext();
        }

        protected TestCarAuctionDbContext GetTestCarAuctionDbContext() =>
            GetNewHostServiceProvider().GetRequiredService<TestCarAuctionDbContext>();

        protected IServiceProvider GetNewHostServiceProvider() =>
            GetTestServer().Services;

        protected TestServer GetTestServer() =>
            new TestServer(new WebHostBuilder().UseStartup<TestStartup>());


    }
}
