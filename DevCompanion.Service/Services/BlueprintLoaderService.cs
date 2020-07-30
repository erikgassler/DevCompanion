using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class BlueprintLoaderService : IBlueprintLoaderService
	{
		public BlueprintLoaderService(
			IFileSystem fileSystem,
			ICryptoService cryptoService,
			IBlueprintRegistryItemListManager registryItemListManager
			)
		{
			FileSystem = fileSystem;
			CryptoService = cryptoService;
			RegistryItemListManager = registryItemListManager;
		}

		public async Task<bool> SaveBlueprint(IBlueprintStorage blueprintStorage)
		{
			SyncSettingsFromBlueprintToStorageObjects(blueprintStorage);
			return await SaveBlueprintToLocalFileSystem(blueprintStorage);
		}

		private void SyncSettingsFromBlueprintToStorageObjects(IBlueprintStorage blueprintStorage)
		{
			if (string.IsNullOrWhiteSpace(blueprintStorage.Blueprint.Name))
			{
				blueprintStorage.Blueprint.Name = $"Id:{blueprintStorage.Id}";
			}
			blueprintStorage.BlueprintRegistryItem.Name = blueprintStorage.Blueprint.Name;
		}

		private Task<bool> SaveBlueprintToLocalFileSystem(IBlueprintStorage blueprintStorage)
		{
			if (string.IsNullOrWhiteSpace(blueprintStorage.BlueprintRegistryItem.Key))
			{
				blueprintStorage.BlueprintRegistryItem.Key = Guid.NewGuid().ToString();
			}
			if (!FileSystem.IsValidDirectory(blueprintStorage.BlueprintRegistryItem.FilePath)) { return Task.FromResult(false); }
			string fileJson = ConvertToJSON(blueprintStorage);
			RegistryItemListManager.PrependRegistryItemToList((BlueprintRegistryItem)blueprintStorage.BlueprintRegistryItem);
			FileSystem.SaveFileToDirectory(blueprintStorage.BlueprintRegistryItem.FilePath, fileJson);
			return Task.FromResult(true);
		}

		private string ConvertToJSON(IBlueprintStorage blueprintStorage)
		{
			string json = JsonConvert.SerializeObject((Blueprint)blueprintStorage.Blueprint);
			blueprintStorage.Data = CryptoService.Encrypt(json, blueprintStorage.BlueprintRegistryItem.Key);
			return JsonConvert.SerializeObject(blueprintStorage);
		}

		public Task<BlueprintStorage> LoadBlueprint(Guid blueprintId)
		{
			BlueprintRegistryItem item = RegistryItemListManager.GetRegistryItem(blueprintId);
			if (item == null) { return Task.FromResult(null as BlueprintStorage); }
			string json = FileSystem.ReadAllText(item.FilePath);
			BlueprintStorage storage = ConvertFromJSON(json, item);
			return Task.FromResult(storage);
		}

		private BlueprintStorage ConvertFromJSON(string fileJson, BlueprintRegistryItem item)
		{
			if (string.IsNullOrWhiteSpace(fileJson)) { return null; }
			BlueprintStorage storage = JsonConvert.DeserializeObject<BlueprintStorage>(fileJson);
			storage.StorageType = Constants.StorageType.Local;
			storage.BlueprintRegistryItem = item;
			string json = CryptoService.Decrypt(storage.Data, item.Key);
			storage.Blueprint = JsonConvert.DeserializeObject<Blueprint>(json);
			return storage;
		}

		private IFileSystem FileSystem { get; }
		private ICryptoService CryptoService { get; }
		private IBlueprintRegistryItemListManager RegistryItemListManager { get; }
	}
}
