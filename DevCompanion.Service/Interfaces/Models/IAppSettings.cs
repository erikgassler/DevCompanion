namespace DevCompanion.Service
{
	public interface IAppSettings
	{
		string LocalEncryptionKey { get; set; }
		bool EnableAutoSyncForCloudBlueprints { get; set; }
		string CloudAPIEndpoint { get; set; }
		string CloudAPILicense { get; set; }

	}
}
