namespace DevCompanion.Service
{
	public class AppSettings : IAppSettings
	{
		public AppSettings(
			IFileSystem fileSystem
			)
		{
			FileSystem = fileSystem;
		}

		public bool EnableAutoSyncForCloudBlueprints { get => GetValue("EnableAutoSyncForCloudBlueprints", false); set => SetValue("EnableAutoSyncForCloudBlueprints", value.ToString()); }
		public string CloudAPIEndpoint { get => GetValue("CloudAPIEndpoint", ""); set => SetValue("CloudAPIEndpoint", value); }
		public string CloudAPILicense { get => GetValue("CloudAPILicense", ""); set => SetValue("CloudAPILicense", value); }

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
	}
}
