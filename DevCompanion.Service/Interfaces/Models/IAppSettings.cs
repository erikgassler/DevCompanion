namespace DevCompanion.Service
{
	public interface IAppSettings
	{
		bool EnableAutoSyncForCloudBlueprints { get; set; }
		string CloudAPIEndpoint { get; set; }
		string CloudAPILicense { get; set; }

	}
}
