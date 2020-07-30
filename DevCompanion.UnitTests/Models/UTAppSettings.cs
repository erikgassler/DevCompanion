using DevCompanion.Service;
using Moq;
using System;
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
				}),
				new CryptoService()
				);

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
				}),
				new CryptoService()
				);

			Assert.True(appSettings.EnableAutoSyncForCloudBlueprints);
		}

		[Fact]
		public void VerifyUpdateAppSetting()
		{
			IAppSettings appSettings = GetAppSettingWithMocks(out IFileSystem fileSystem);

			// Cloud License
			string savingValue = "CloudLicenseValue";
			Assert.Equal("", appSettings.CloudAPILicense);
			appSettings.CloudAPILicense = savingValue;
			Assert.True(fileSystem.TryGetRegistryValue("CloudAPILicense", out string savedLicense));
			Assert.Equal(savingValue, savedLicense);
			Assert.Equal(savingValue, appSettings.CloudAPILicense);

			// Local Encryption Key
			savingValue = "LocalEncValue";
			Assert.Equal("", appSettings.LocalEncryptionKey);
			appSettings.LocalEncryptionKey = savingValue;
			Assert.True(fileSystem.TryGetRegistryValue("LocalEncryptionKey", out string savedEncKey));
			Assert.Equal(savingValue, savedEncKey);
			Assert.Equal(savingValue, appSettings.LocalEncryptionKey);
		}
		[Fact]
		public void VerifySetAndRetrieveBlueprintList()
		{
			IAppSettings appSettings = GetAppSettingWithMocks(out IFileSystem fileSystem);
			Assert.Empty(appSettings.BlueprintList);
			var registryItem = new BlueprintRegistryItem() {
				Id = Guid.NewGuid(),
				Key = "Shhh this is a secret!"
			};
			appSettings.BlueprintList.Add(registryItem);

			// overwriting settings with new settings to rebuild blueprint list from storage so we can assure it loads correctly
			appSettings = new AppSettings(fileSystem, new CryptoService());
			Assert.Single(appSettings.BlueprintList);
			Assert.Equal(registryItem.Id, appSettings.BlueprintList[0].Id);
			Assert.Equal(registryItem.Key, appSettings.BlueprintList[0].Key);

			// Assure stored values are encrypted
			Assert.True(fileSystem.TryGetRegistryValue("BlueprintList", out string savedList));
			Assert.DoesNotContain("Shhh this is a secret", savedList);
		}
	}
}
