using System.Threading.Tasks;
using CarAuction.Tests.Shared;
using Microsoft.Extensions.DependencyInjection;
using Services.Profile;
using Xunit;

namespace CarAuction.Tests.Services.Profile
{
    public class ProfileServiceTests : TestBase
    {
        private readonly IProfileService _profileService;

        public ProfileServiceTests()
        {
            _profileService = GetNewHostServiceProvider().GetRequiredService<IProfileService>();
        }

        public async Task 
    }
}
