using DevCompanion.Service;
using System.Threading.Tasks;
using Xunit;

namespace DevCompanion.BuildTests.Services
{
	public class UTMemoryFileSystem
	{
		[Fact]
		public async Task VerifyMemoryFileSystemDoesNotBreak()
		{
			PlaceholderFileSystem service = new PlaceholderFileSystem();
			Assert.False(service.Exists(service.GetFullPath("./")));
			Assert.Equal("", await service.ReadAllText(""));
			service.SetRegistryValue("TestKey", "One");
			Assert.True(service.TryGetRegistryValue("TestKey", out string result));
			Assert.Equal("One", result);
		}
	}
}
