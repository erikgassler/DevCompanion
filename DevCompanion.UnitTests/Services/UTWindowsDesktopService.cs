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
			IDesktopService service = new WindowsDesktopService();
			service.InitializeDesktop(
				Mock<IDesktopWindow>(mock =>
				{
				}));
			service.StopDesktopServices();
		}

		[Fact]
		public void VerifyCreateNewBlueprint()
		{
			IDesktopService service = new WindowsDesktopService();
			service.CreateNewBlueprint();
		}

		[Fact]
		public void VerifyOpenBlueprint()
		{
			IDesktopService service = new WindowsDesktopService();
			service.OpenBlueprint();
		}

		[Fact]
		public void VerifySaveBlueprint()
		{
			IDesktopService service = new WindowsDesktopService();
			service.SaveBlueprint();
		}

		[Fact]
		public void VerifySyncBlueprint()
		{
			IDesktopService service = new WindowsDesktopService();
			service.SyncBlueprint();
		}
	}
}
