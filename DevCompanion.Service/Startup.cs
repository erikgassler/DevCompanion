using DevCompanion.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevCompanion.Service
{
	public class Startup : IDisposable
	{
		#region Static Methods & Properties
		public static T GetService<T>()
		{
			Instance ??= new Startup(); // Setup singleton if null
			return Instance.Provider.GetService<T>();
		}

		/// <summary>
		/// Method to call when app is closing to save any state and dispose of disposable objects.
		/// </summary>
		public static void CloseServices()
		{
			Instance?.Dispose();
		}

		private static Startup Instance { get; set; }
		#endregion

		public Startup()
		{
			IServiceCollection service = SetupConfiguredServices();
			Provider = service.BuildServiceProvider();
		}

		private IServiceCollection SetupConfiguredServices()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<IDesktopService, WindowsDesktopService>();
			return services;
		}

		public IServiceProvider Provider { get; set; }

		public void Dispose()
		{
			((ServiceProvider)Provider)?.Dispose();
			Provider = null;
		}
	}
}
