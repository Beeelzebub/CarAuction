using System.Threading.Tasks;
using CarAuction.Tests.Shared;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Services.Profile;
using Xunit;

namespace CarAuction.Tests.Services.Profile
{
    public class ProfileServiceTests : TestBase
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<User> _userManager;

        public ProfileServiceTests()
        {
            var serviceProvider = GetNewHostServiceProvider();
            _profileService = serviceProvider.GetRequiredService<IProfileService>();
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        }
    }
}
