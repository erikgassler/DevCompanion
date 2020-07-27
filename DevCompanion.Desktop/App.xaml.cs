using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
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
				services.AddSingleton<IFileSystem, FileSystem>();
			};
		}
	}
}
