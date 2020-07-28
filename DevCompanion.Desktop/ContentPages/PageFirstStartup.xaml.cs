using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevCompanion.Desktop.StaticContent
{
	/// <summary>
	/// Interaction logic for FirstStartup.xaml
	/// </summary>
	public partial class FirstStartup : UserControl
	{
		public FirstStartup(ServiceProvider provider)
		{
			Provider = provider;
			DesktopWindow = provider.GetService<IDesktopWindow>();
			AppSettings = provider.GetService<IAppSettings>();
			InitializeComponent();
			SetupPasswordField();
			DesktopWindow.UpdateStatus("Get Started by creating or opening your first Blueprint!");
		}

		private void SetupPasswordField()
		{
			LocalPassword.Text = AppSettings.LocalEncryptionKey;
			LocalPassword.TextChanged += (object sender, TextChangedEventArgs e)=> AppSettings.LocalEncryptionKey = LocalPassword.Text;
		}

		private IDesktopWindow DesktopWindow { get; }
		private IAppSettings AppSettings { get; }
		private ServiceProvider Provider { get; }
	}
}
