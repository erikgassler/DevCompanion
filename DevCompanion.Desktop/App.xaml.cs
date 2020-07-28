using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Windows;

namespace DevCompanion.Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
			: base()
		{
			Service.Startup.ClientServices = services =>
			{
				services.Replace(new ServiceDescriptor(typeof(IFileSystem), typeof(FileSystem), ServiceLifetime.Singleton));
			};
		}
	}
}
