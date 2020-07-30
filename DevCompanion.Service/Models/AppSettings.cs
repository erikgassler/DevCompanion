using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DevCompanion.Service
{
	public class AppSettings : IAppSettings
	{
		public AppSettings(
			IFileSystem fileSystem,
			ICryptoService cryptoService
			)
		{
			FileSystem = fileSystem;
			CryptoService = cryptoService;
			BlueprintList = new ObservableCollection<BlueprintRegistryItem>();
			PopulateBlueprintListFromStorage();
			UpdateBlueprintListOnChanges();
		}
		public event EventHandler<BlueprintRegistryItem[]> OnUpdateBlueprintList;

		private void PopulateBlueprintListFromStorage()
		{
			try
			{
				string encryptedData = GetValue("BlueprintList", "");
				if (string.IsNullOrEmpty(encryptedData)) { return; }
				string json = CryptoService.Decrypt(encryptedData, AppSettingsKey);
				BlueprintRegistryItem[] items = JsonConvert.DeserializeObject<BlueprintRegistryItem[]>(json);
				if(items == null) { return; }
				foreach(BlueprintRegistryItem item in items)
				{
					BlueprintList.Add(item);
				}
			}
			catch (Exception) { }
		}

		private void UpdateBlueprintListOnChanges()
		{
			BlueprintList.CollectionChanged += BlueprintList_CollectionChanged;
		}

		private void BlueprintList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			string json = JsonConvert.SerializeObject(BlueprintList);
			SetValue("BlueprintList", CryptoService.Encrypt(json, AppSettingsKey));
			OnUpdateBlueprintList?.Invoke(this, BlueprintList.ToArray());
		}

		public string LocalEncryptionKey { get => GetValue("LocalEncryptionKey", ""); set => SetValue("LocalEncryptionKey", value); }
		public bool EnableAutoSyncForCloudBlueprints { get => GetValue("EnableAutoSyncForCloudBlueprints", false); set => SetValue("EnableAutoSyncForCloudBlueprints", value.ToString()); }
		public string CloudAPIEndpoint { get => GetValue("CloudAPIEndpoint", ""); set => SetValue("CloudAPIEndpoint", value); }
		public string CloudAPILicense { get => GetValue("CloudAPILicense", ""); set => SetValue("CloudAPILicense", value); }
		public string DefaultSaveFolder { get => GetValue("DefaultSaveFolder", ""); set => SetValue("DefaultSaveFolder", value.Replace(@"\\", "/").Replace(@"\", "/")); }
		public ObservableCollection<BlueprintRegistryItem> BlueprintList { get; }

		private bool GetValue(string key, bool defaultValue)
		{
			if(FileSystem.TryGetRegistryValue(key, out string value)
				&& bool.TryParse(value, out bool result))
			{
				return result;
			}
			return defaultValue;
		}

		private void SetValue(string key, string value)
		{
			FileSystem.SetRegistryValue(key, value);
		}

		private string GetValue(string key, string defaultValue)
		{
			if(FileSystem.TryGetRegistryValue(key, out string value))
			{
				return value;
			}
			return defaultValue;
		}

		private IFileSystem FileSystem { get; }
		private ICryptoService CryptoService { get; }
		private const string AppSettingsKey = "Pq38X8sn2(xnwi@9!9xnw";
	}
}
