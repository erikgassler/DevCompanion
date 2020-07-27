using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
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

		[Fact]
		public void VerifyLoadingUserAppSettings()
		{
			Startup.ClientServices = services =>
			{
				services.AddSingleton<IFileSystem, MockFileSystem>();
			};
			IUserAppSettings settings = Startup.GetService<IUserAppSettings>();
		}

		private class MockFileSystem : IFileSystem
		{
			public bool Exists(string filePath)
			{
				return true;
			}

			public string GetFullPath(string partialPath)
			{
				return $"MockPath/{partialPath}";
			}

			public Task<string> ReadAllTextAsync(string filePath)
			{
				return Task.FromResult("");
			}
		}
	}
}
