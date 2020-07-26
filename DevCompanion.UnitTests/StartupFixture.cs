using DevCompanion.Service;
using System;

namespace DevCompanion.UnitTests
{
	/// <summary>
	/// Class for Fixture setups to assure Startup services can be requested if desired for tests and disposing of services will only happen after all tests are finished.
	/// IClassFixture<StartupFixture> when Startup is only used by a single tests class.
	/// ICollectionDefinition<StartupFixture> when Startup is used by multiple test classes.
	/// </summary>
	public class StartupFixture : IDisposable
	{
		public void Dispose()
		{
			Startup.CloseServices();
		}
	}
}
