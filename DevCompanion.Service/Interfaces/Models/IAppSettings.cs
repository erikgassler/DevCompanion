using System;
using System.Collections.ObjectModel;

namespace DevCompanion.Service
{
	public interface IAppSettings
	{
		string LocalEncryptionKey { get; set; }
		bool EnableAutoSyncForCloudBlueprints { get; set; }
		string CloudAPIEndpoint { get; set; }
		string CloudAPILicense { get; set; }
		string DefaultSaveFolder { get; set; }
		ObservableCollection<BlueprintRegistryItem> BlueprintList { get; }
		event EventHandler<BlueprintRegistryItem[]> OnUpdateBlueprintList;
	}
}
