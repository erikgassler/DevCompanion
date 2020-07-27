using Xunit;
using DevCompanion.Service;
using System.Threading.Tasks;
using DevCompanion.BuildTests;
using Moq;

namespace DevCompanion.UnitTests.Services
{
	public class UIAppSettingsLoader : MockHelpers
	{
		[Fact]
		public void VerifyLoadAppSettingsMissingFile()
		{
			var result = RunServiceAndGetResult(null, false).GetAwaiter().GetResult();
			Assert.Null(result);
		}

		[Fact]
		public void VerifyLoadAppSettingsFileFound()
		{
			var result = RunServiceAndGetResult(@"
				{
					""EnableAutoSyncForCloudBlueprints"": true
				}
				", true).GetAwaiter().GetResult();
			Assert.NotNull(result);
		}

		[Fact]
		public void VerifyLoadAppSettingsFileFoundWithInvalidJSON()
		{
			var result = RunServiceAndGetResult("---", true).GetAwaiter().GetResult();
			Assert.Null(result);
		}

		private async Task<IAppSettings> RunServiceAndGetResult(string mockFile, bool fileExists)
		{
			IAppSettingsLoader service = new AppSettingsLoader(
				Mock<IFileSystem>(mock =>
				{
					mock.Setup(m => m.Exists(It.IsAny<string>())).Returns(fileExists);
					mock.Setup(m => m.ReadAllTextAsync(It.IsAny<string>())).Returns(Task.FromResult(mockFile));
				})
				);
			return await service.LoadAppSettingsFromJSON();
		}
	}
}
