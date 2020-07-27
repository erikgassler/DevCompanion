using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DevCompanion.UnitTests
{
	public class UTStartup : IClassFixture<StartupFixture>
	{
		public UTStartup()
		{
			Startup.ClientServices = services =>
			{
				services.AddSingleton<IFileSystem, MockFileSystem>();
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

			public void SetRegistryValue(string key, string value)
			{
				Registry[key] = value;
			}

			public bool TryGetRegistryValue(string key, out string value)
			{
				return Registry.TryGetValue(key, out value);
			}

			private Dictionary<string, string> Registry = new Dictionary<string, string>();
		}
	}
}
