using DevCompanion.Service;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DevCompanion.BuildTests
{
	public class UTAppSettings : MockHelpers
	{
		[Fact]
		public void VerifyAppSettingsNotSet()
		{
			IAppSettings appSettings = new AppSettings(
				Mock<IFileSystem>(mock => { 
				}));

			Assert.False(appSettings.EnableAutoSyncForCloudBlueprints);
			Assert.Equal("", appSettings.CloudAPIEndpoint);
			Assert.Equal("", appSettings.CloudAPILicense);
		}

		[Fact]
		public void VerifyAppSettingsSet()
		{
			string autoSyncText = "True";
			IAppSettings appSettings = new AppSettings(
				Mock<IFileSystem>(mock => {
					mock.Setup(m => m.TryGetRegistryValue("EnableAutoSyncForCloudBlueprints", out autoSyncText)).Returns(true);
				}));

			Assert.True(appSettings.EnableAutoSyncForCloudBlueprints);
		}

		public delegate void CallbackDelegate(string input, out string output);

		[Fact]
		public void VerifyUpdateAppSetting()
		{
			Dictionary<string, string> fileSystem = new Dictionary<string, string>();
			IAppSettings appSettings = new AppSettings(
				Mock<IFileSystem>(mock => {
					mock.Setup(m => m.SetRegistryValue(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, valueToSave) =>
					{
						fileSystem[key] = valueToSave;
					});
					string value = null;
					mock.Setup(m => m.TryGetRegistryValue(It.IsAny<string>(), out value))
						.Callback(new CallbackDelegate((string key, out string outValue) => 
						{ 
							outValue = fileSystem.ContainsKey(key) ? fileSystem[key] : ""; 
						}))
						.Returns(true);
				}));

			// Cloud License
			string savingValue = "CloudLicenseValue";
			Assert.Equal("", appSettings.CloudAPILicense);
			appSettings.CloudAPILicense = savingValue;
			Assert.Equal(savingValue, fileSystem["CloudAPILicense"]);
			Assert.Equal(savingValue, appSettings.CloudAPILicense);

			// Local Encryption Key
			savingValue = "LocalEncValue";
			Assert.Equal("", appSettings.LocalEncryptionKey);
			appSettings.LocalEncryptionKey = savingValue;
			Assert.Equal(savingValue, fileSystem["LocalEncryptionKey"]);
			Assert.Equal(savingValue, appSettings.LocalEncryptionKey);
		}
	}
}
