using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace DevCompanion.BuildTests
{
	public class MockHelpers
	{
		public T Mock<T>(Action<Mock<T>> setupHandler = null)
			where T: class
		{
			Mock<T> mock = new Mock<T>();
			setupHandler?.Invoke(mock);
			return mock.Object;
		}

		public IServiceProvider GetMockProvider(
			out IAppSettings appSettings,
			out IFileSystem fileSystem,
			out IBlueprintLoaderService loaderService
			)
		{
			appSettings = GetAppSettingWithMocks(out fileSystem);
			ICryptoService refCrypto = new CryptoService();
			IBlueprintRegistryItemListManager refListManager = new BlueprintRegistryItemListManager(appSettings);
			loaderService = new BlueprintLoaderService(
				fileSystem,
				refCrypto,
				refListManager
				);
			IAppSettings refSettings = appSettings;
			IFileSystem refSystem = fileSystem;
			IBlueprintLoaderService refLoader = loaderService;
			return Mock<IServiceProvider>(mock =>
			{
				mock.Setup(m => m.GetService(It.IsAny<Type>()))
					.Returns((Type type) =>
					{
						return type.Name switch
						{
							"IBlueprintRegistryItemListManager" => refListManager,
							"ICryptoService" => refCrypto,
							"IBlueprintLoaderService" => refLoader,
							"IAppSettings" => refSettings,
							"IFileSystem" => refSystem,
							"IBlueprint" => new Blueprint(),
							_ => null
						};
					});
			});
		}

		public IAppSettings GetAppSettingWithMocks(out IFileSystem fileSystem)
		{
			fileSystem = new PlaceholderFileSystem();
			return new AppSettings(
				fileSystem,
				new CryptoService()
				);
		}
		public delegate void CallbackDelegate(string input, out string output);
	}
}
