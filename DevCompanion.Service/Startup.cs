using DevCompanion.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevCompanion.Service
{
	public class Startup : IDisposable
	{
		#region Static Methods & Properties
		public static ServiceProvider GetProvider()
		{
			Instance ??= new Startup(); // Setup singleton if null
			return Instance.Provider;
		}

		/// <summary>
		/// Set by Client IDesktopWindow control to replace Placeholder instances with Client instances.
		/// </summary>
		public static Action<IServiceCollection> ClientServices { get; set; }

		public delegate void HandleServicesReady(ServiceProvider provider);
		public static event HandleServicesReady OnServicesReady;
		public static void DesktopServicesAreReady()
		{
			OnServicesReady.Invoke(Instance.Provider);
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

		/// <summary>
		/// This class is never directly accessible.
		/// It's purpose is to build the collection services that handle managing dependency injection.
		/// </summary>
		private Startup()
		{
			Provider = SetupConfiguredServices().BuildServiceProvider();
		}

		/// <summary>
		/// Map classes to be injected into constructors for applicable interfaces/classes.
		/// </summary>
		/// <returns></returns>
		private IServiceCollection SetupConfiguredServices()
		{
			IServiceCollection services = new ServiceCollection();
			SetupClientServices(services);
			SetupAppConfigurations(services);
			SetupModelServices(services);
			// This goes last so UI can replace services if needed
			ClientServices?.Invoke(services);
			return services;
		}

		/// <summary>
		/// Add the main services that represent the main entry point for accessing UI accessible processes.
		/// </summary>
		/// <param name="services"></param>
		private void SetupClientServices(IServiceCollection services)
		{
			services.AddSingleton<IDesktopService, WindowsDesktopService>();
		}

		private void SetupModelServices(IServiceCollection services)
		{
			services.AddTransient<IBlueprint, Blueprint>();
			services.AddSingleton<IFileSystem, PlaceholderFileSystem>();
			services.AddSingleton<IDesktopWindow, PlaceholderDesktopWindow>();
			services.AddTransient<IUnitTypeAzureAppConfig, UnitTypeAzureAppConfig>();
			services.AddTransient<IUnitTypeAzureKeyVault, UnitTypeAzureKeyVault>();
			services.AddTransient<IUnitTypeBlueprint, UnitTypeBlueprint>();
			services.AddTransient<IUnitTypeCommandPromptScript, UnitTypeCommandPromptScript>();
			services.AddTransient<IUnitTypeDocumentation, UnitTypeDocumentation>();
			services.AddTransient<IUnitTypeEnvironmentVariable, UnitTypeEnvironmentVariable>();
			services.AddTransient<IUnitTypePowerShellScript, UnitTypePowerShellScript>();
			services.AddTransient<IUnitTypeWorkflow, UnitTypeWorkflow>();
		}

		private void SetupAppConfigurations(IServiceCollection services)
		{
			services.AddSingleton<IAppSettings, AppSettings>();
		}

		private ServiceProvider Provider { get; set; }

		public void Dispose()
		{
			Provider?.Dispose();
			Provider = null;
		}
	}
}
