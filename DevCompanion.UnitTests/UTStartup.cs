using DevCompanion.Service;
using System;
using Xunit;

namespace DevCompanion.UnitTests
{
	public class UTStartup : IDisposable
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
			IServiceProvider provider = Startup.GetService<IServiceProvider>();
			Assert.NotNull(provider);
		}

		[Fact]
		public void VerifyCanAccessServiceProviderIfNeeded()
		{
			IServiceProvider provider = Startup.GetService<IServiceProvider>();
			Assert.NotNull(provider);
		}

		[Fact]
		public void VerifyLoadingUserAppSettings()
		{
			IAppSettings settings = Startup.GetService<IAppSettings>();
		}

		public void Dispose()
		{
			Startup.CloseServices();
		}
	}
}
