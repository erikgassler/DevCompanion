using DevCompanion.Service;
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

namespace DevCompanion.Desktop.Components
{
	/// <summary>
	/// Interaction logic for TopMenuBar.xaml
	/// </summary>
	public partial class TopMenuBar : UserControl
	{
		public TopMenuBar()
		{
			InitializeComponent();
			AppSettings = Startup.GetService<IAppSettings>();
			ApplySettings();
		}

		private void ApplySettings()
		{
			ApplyCloudAPIEndpointSetting();
			ApplyCloudAPILicenseSetting();
			ApplyAutoSyncSetting();
			LoadBlueprintListItems();
		}

		private void ApplyCloudAPIEndpointSetting()
		{
			this.CloudStorageAPIEndpoint.RefValue = AppSettings.CloudAPIEndpoint;
			this.CloudStorageAPIEndpoint.RefValueUpdated += CloudStorageAPIEndpoint_RefValueUpdated;
		}

		private void CloudStorageAPIEndpoint_RefValueUpdated(object sender, EventArgs e)
		{
			AppSettings.CloudAPIEndpoint = ((MenuItemTextInput)sender).RefValue;
		}

		private void ApplyCloudAPILicenseSetting()
		{
			this.CloudStorageAPILicense.RefValue = AppSettings.CloudAPILicense;
			this.CloudStorageAPILicense.RefValueUpdated += CloudStorageAPILicense_RefValueUpdated;
		}

		private void CloudStorageAPILicense_RefValueUpdated(object sender, EventArgs e)
		{
			AppSettings.CloudAPILicense = ((MenuItemTextInput)sender).RefValue;
		}

		private void ApplyAutoSyncSetting()
		{
			this.AutoSyncBlueprintToggle.IsChecked = AppSettings.EnableAutoSyncForCloudBlueprints;
			this.AutoSyncBlueprintToggle.StaysOpenOnClick = true;
			this.AutoSyncBlueprintToggle.Click += AutoSyncBlueprintToggle_Click;
		}

		private void AutoSyncBlueprintToggle_Click(object sender, RoutedEventArgs e)
		{
			AppSettings.EnableAutoSyncForCloudBlueprints = this.AutoSyncBlueprintToggle.IsChecked;
		}

		private IAppSettings AppSettings { get; }

		private void LoadBlueprintListItems()
		{
			// Clear current list
			this.BlueprintList.Items.Clear();
			// Load with dummy items for now.
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 1" });
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 2" });
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 3" });
		}

		private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.Close();
		}

		private void MenuItem_BlueprintListUpdated(object sender, DataTransferEventArgs e)
		{

		}
	}
}
