using DevCompanion.Service;
using System;
using Xunit;

namespace DevCompanion.UnitTests
{
	public class UTStartup : IClassFixture<StartupFixture>
	{
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
	}
}
