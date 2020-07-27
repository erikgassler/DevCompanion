namespace DevCompanion.Service
{
	public class AppSettings : IAppSettings, IUserAppSettings, IDefaultAppSettings
	{
		public AppSettings(IAppSettings defaultSettings = null)
		{
			this.EnableAutoSyncForCloudBlueprints = defaultSettings?.EnableAutoSyncForCloudBlueprints ?? false;
		}

		public bool EnableAutoSyncForCloudBlueprints { get; set; }
	}
}
