using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
using System.Threading;
using Xunit;

namespace DevCompanion.BuildTests
{
	public class ITWindowsDesktopService : MockHelpers
	{
		[Fact]
		public void VerifyDesktopServiceStarts()
		{
			IDesktopService service = Startup.GetService<IDesktopService>();
			service.InitializeDesktop(
				Mock<IDesktopWindow>(mock =>
				{

				}));
			Thread.Sleep(5000);
			service.StopDesktopServices();
		}
	}
}
