using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuction.IntegrationTests.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.IntegrationTests
{
    public abstract class TestBase 
    {
        protected readonly TestServer _server;

        protected TestBase()
        {
            _server = GetTestServer();
        }

        protected TestCarAuctionDbContext GetTestCarAuctionDbContext() =>
            GetNewHostServiceProvider().GetRequiredService<TestCarAuctionDbContext>();

        protected IServiceProvider GetNewHostServiceProvider() =>
            _server.Services;

        protected TestServer GetTestServer() =>
            new TestServer(new WebHostBuilder().UseStartup<TestStartup>());

    }
}
