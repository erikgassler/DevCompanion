using DevCompanion.Service;
using System.Threading.Tasks;
using Xunit;

namespace DevCompanion.BuildTests.Services
{
	public class UTBlueprintLoaderService : MockHelpers
	{
		[Fact]
		public async Task VerifySaveBlueprint()
		{
			IBlueprintLoaderService service = GetBlueprintLoaderService(out IAppSettings appSettings);
			IBlueprintStorage storage = GetBlueprintStorage("Test A");
			Assert.True(await service.SaveBlueprint(storage));
			// blueprint is expected to be added to registry list that is used to remember and load blueprints from
			Assert.Single(appSettings.BlueprintList);
		}

		[Fact]
		public async Task VerifyLoadBlueprint()
		{
			IBlueprintLoaderService service = GetBlueprintLoaderService(out IAppSettings appSettings);
			// First we need to save our test blueprint so we have something to load
			IBlueprintStorage storage = GetBlueprintStorage("Test A");
			Assert.True(await service.SaveBlueprint(storage));

			// Load first blueprint from list in app settings
			BlueprintStorage loaded = await service.LoadBlueprint(appSettings.BlueprintList[0].Id);
			Assert.Equal(storage.Id, loaded.BlueprintRegistryItem.Id);
			Assert.Equal(storage.Blueprint.Name, loaded.Blueprint.Name);
		}

		private IBlueprintStorage GetBlueprintStorage(string name)
		{
			IBlueprint blueprint = new Blueprint()
			{
				Name = name
			};
			BlueprintRegistryItem registryItem = new BlueprintRegistryItem()
			{
				Id = blueprint.Id,
				Name = name
			};
			registryItem.SetFilePath("./");
			return new BlueprintStorage()
			{
				Id = blueprint.Id,
				Blueprint = blueprint,
				BlueprintRegistryItem = registryItem
			};
		}

		private IBlueprintLoaderService GetBlueprintLoaderService(out IAppSettings appSettings)
		{
			appSettings = GetAppSettingWithMocks(out IFileSystem fileSystem);
			return new BlueprintLoaderService(
				fileSystem,
				new CryptoService(),
				new BlueprintRegistryItemListManager(appSettings)
				);
		}
	}
}
