using DevCompanion.Service;
using System;
using Xunit;

namespace DevCompanion.BuildTests
{
	public class ITWindowsDesktopService : MockHelpers
	{
		[Fact]
		public void VerifyServiceLifecycle()
		{
			IDesktopService service = new WindowsDesktopService(
				GetMockProvider(out IAppSettings appSettings, out _, out _),
				appSettings
				);
			service.RunStartup();
			service.CreateNewBlueprint();
			service.SaveBlueprint();
			service.OpenBlueprint(service.ActiveBlueprintStorage.Id);
			service.CloseApplication();
		}

		[Fact]
		public void VerifyStartupAndDispose()
		{
			IDesktopService service = new WindowsDesktopService(
				GetMockProvider(out IAppSettings appSettings, out _, out _),
				appSettings
				);
			service.CloseApplication();
		}

		[Fact]
		public void VerifyCreateNewBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				GetMockProvider(out IAppSettings appSettings, out _, out _),
				appSettings
				);
			service.CreateNewBlueprint();
			service.CloseApplication();
		}

		[Fact]
		public void VerifySaveBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				GetMockProvider(out IAppSettings appSettings, out _, out _),
				appSettings
				);
			service.SaveBlueprint();
			service.CloseApplication();
		}

		[Fact]
		public void VerifySyncBlueprint()
		{
			IDesktopService service = new WindowsDesktopService(
				GetMockProvider(out IAppSettings appSettings, out _, out _),
				appSettings
				);
			service.SyncBlueprint();
			service.CloseApplication();
		}
	}
}
