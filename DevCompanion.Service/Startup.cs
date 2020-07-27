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
			return Instance.GetProviderService<T>();
		}

		public static Action<IServiceCollection> ClientServices { get; set; }

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
		}

		/// <summary>
		/// Extra wrapper method for accessing Provider.GetService to assure Provider has not been disposed.
		/// This is more for unit tests (at least with NCrunch) which seems to retest updates using old instances that already ran CloseServices() which disposed of Provider.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		private T GetProviderService<T>()
		{
			Provider ??= SetupConfiguredServices().BuildServiceProvider();
			return Provider.GetService<T>();
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

		private IServiceProvider Provider { get; set; }

		public void Dispose()
		{
			((ServiceProvider)Provider)?.Dispose();
			Provider = null;
		}
	}
}
