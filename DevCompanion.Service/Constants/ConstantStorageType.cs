namespace DevCompanion.Service
{
	public partial class Constants
	{
		public enum StorageType
		{
			[DisplayName("Not Saved")]
			UnSaved = 0,
			[DisplayName("Saved Locally")]
			Local = 1,
			[DisplayName("Saved in Cloud")]
			Cloud = 2
		}
	}
}
