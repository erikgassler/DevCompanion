using DevCompanion.Service;
using Xunit;

namespace DevCompanion.UnitTests
{
	public class UTEnumAttributes
	{
		[Theory]
		[InlineData("Blueprint", Constants.UnitType.Blueprint)]
		[InlineData("Azure App Config", Constants.UnitType.AzureAppConfig)]
		[InlineData("Azure Key Vault", Constants.UnitType.AzureKeyVault)]
		[InlineData("Command Prompt Script", Constants.UnitType.CommandPromptScript)]
		[InlineData("Documentation", Constants.UnitType.Documentation)]
		[InlineData("Environment Variable", Constants.UnitType.EnvironmentVariable)]
		[InlineData("PowerShell Script", Constants.UnitType.PowerShellScript)]
		[InlineData("Workflow", Constants.UnitType.Workflow)]
		public void VerifyDisplayNameOutput(string expectedDisplay, Constants.UnitType enumValue)
		{
			Assert.Equal(expectedDisplay, enumValue.DisplayName());
		}
	}
}
