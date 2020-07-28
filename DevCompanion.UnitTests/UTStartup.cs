using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DevCompanion.UnitTests
{
	public class UTStartup
	{
		public UTStartup()
		{
			Startup.ClientServices = services =>
			{
				// Can replace services here if needed
			};
		}

		[Fact]
		public void VerifyDesktopService()
		{
			ServiceProvider provider = Startup.GetProvider();
			Assert.NotNull(provider);
		}

		[Fact]
		public void VerifyCanAccessServiceProviderIfNeeded()
		{
			ServiceProvider provider = Startup.GetProvider();
			Assert.NotNull(provider);
		}

		[Fact]
		public void VerifyLoadingUserAppSettings()
		{
			ServiceProvider provider = Startup.GetProvider();
			IAppSettings settings = provider.GetService<IAppSettings>();
		}
	}
}
