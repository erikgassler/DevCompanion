using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
using System;
using System.Threading;
using Xunit;

namespace DevCompanion.BuildTests
{
	public class UTWindowsDesktopService : MockHelpers
	{
		[Fact]
		public void VerifyStartupAndDispose()
		{
			IDesktopService service = new WindowsDesktopService(
				Mock<IServiceProvider>(mock => { }),
				Mock<IDesktopWindow>(mock => { })
				);
			service.StopDesktopServices();
		}

		[Fact]
		public void VerifyCreateNewBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				Mock<IServiceProvider>(mock => { }),
				Mock<IDesktopWindow>(mock => { })
				);
			service.CreateNewBlueprint();
			service.StopDesktopServices();
		}

		[Fact]
		public void VerifyOpenBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				Mock<IServiceProvider>(mock => { }),
				Mock<IDesktopWindow>(mock => { })
				);
			service.OpenBlueprint();
			service.StopDesktopServices();
		}

		[Fact]
		public void VerifySaveBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				Mock<IServiceProvider>(mock => { }),
				Mock<IDesktopWindow>(mock => { })
				);
			service.SaveBlueprint();
			service.StopDesktopServices();
		}

		[Fact]
		public void VerifySyncBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				Mock<IServiceProvider>(mock => { }),
				Mock<IDesktopWindow>(mock => { })
				);
			service.SyncBlueprint();
			service.StopDesktopServices();
		}
	}
}
