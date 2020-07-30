using DevCompanion.Desktop.ContentPages;
using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace DevCompanion.Desktop.StaticContent
{
	/// <summary>
	/// Interaction logic for FirstStartup.xaml
	/// </summary>
	public partial class PageFirstStartup : BaseContentPage
	{
		public PageFirstStartup(ServiceProvider provider)
		{
			Provider = provider;
			DesktopService = provider.GetService<IDesktopService>();
			AppSettings = provider.GetService<IAppSettings>();
			InitializeComponent();
			SetupPasswordField();
			DesktopService.UpdateStatus("Get Started by creating or opening your first Blueprint!");
		}

		public override void UnloadForRemoval()
		{
		}

		private void SetupPasswordField()
		{
			LocalPassword.Text = AppSettings.LocalEncryptionKey;
			LocalPassword.TextChanged += (object sender, TextChangedEventArgs e)=> AppSettings.LocalEncryptionKey = LocalPassword.Text;
		}

		private IDesktopService DesktopService { get; }
		private IAppSettings AppSettings { get; }
		private ServiceProvider Provider { get; }
	}
}
