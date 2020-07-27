using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
using Xunit;

namespace DevCompanion.BuildTests
{
	public class ITWindowsDesktopService
	{
		[Fact]
		public void VerifyDesktopServiceStarts()
		{
			IDesktopService service = Startup.GetService<IDesktopService>();
		}
	}
}
