namespace DevCompanion.Service
{
	public partial class Constants
	{
		public enum UnitType
		{
			Documentation = 1,
			[DisplayName("Environment Variable")]
			EnvironmentVariable = 2,
			[DisplayName("Azure Key Vault")]
			AzureKeyVault = 3,
			[DisplayName("Azure App Config")]
			AzureAppConfig = 4,
			[DisplayName("Command Prompt Script")]
			CommandPromptScript = 5,
			[DisplayName("PowerShell Script")]
			PowerShellScript = 6,
			Workflow = 7,
			Blueprint = 8
		}
	}
}
