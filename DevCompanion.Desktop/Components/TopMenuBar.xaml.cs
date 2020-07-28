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
			SetupEventHandlers();
			ApplySettings();
			DesktopWindow = (IDesktopWindow)Application.Current.MainWindow;
		}

		#region Menu Click Handlers
		private void AutoSyncBlueprintToggle_Click(object sender, RoutedEventArgs e)
		{
			AppSettings.EnableAutoSyncForCloudBlueprints = this.AutoSyncBlueprintToggle.IsChecked;
		}

		private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
		{
			DesktopWindow.CloseApplication();
		}
		private void MenuItem_ClickMinMaxResize(object sender, RoutedEventArgs e)
		{
			if (IsCurrentState(WindowState.Maximized))
			{
				UpdateWindowState(WindowState.Normal);
			}
			else
			{
				UpdateWindowState(WindowState.Maximized);
			}
		}

		private void MenuItem_ClickMinimize(object sender, RoutedEventArgs e)
		{
			UpdateWindowState(WindowState.Minimized);
		}

		private void MenuItem_ClickCreateBlueprint(object sender, RoutedEventArgs e)
		{

		}

		private void MenuItem_ClickOpenBlueprint(object sender, RoutedEventArgs e)
		{

		}

		private void MenuItem_ClickSaveBlueprint(object sender, RoutedEventArgs e)
		{

		}

		private void MenuItem_ClickSyncBlueprint(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		#region Event Handlers
		private void SetupEventHandlers()
		{
			Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;
		}
		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
			{
				ButtonMinMax.Header = "_❐";
				ButtonMinMax.ToolTip = "Restore Down";
			}
			else
			{
				ButtonMinMax.Header = "_☐";
				ButtonMinMax.ToolTip = "Maximize";
			}
		}
		#endregion

		#region Menu Setup from loaded settings
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
		private void LoadBlueprintListItems()
		{
			// Clear current list
			this.BlueprintList.Items.Clear();
			// Load with dummy items for now.
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 1" });
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 2" });
			this.BlueprintList.Items.Add(new MenuItem() { Header = "_Dummy Blueprint 3" });
		}
		#endregion

		#region MainWindow interactions
		private void UpdateWindowState(WindowState state)
		{
			Application.Current.MainWindow.WindowState = state;
		}
		private bool IsCurrentState(WindowState state)
		{
			return Application.Current.MainWindow.WindowState == state;
		}
		#endregion

		#region Object References
		private IDesktopWindow DesktopWindow { get; }
		private IAppSettings AppSettings { get; }
		#endregion
	}
}
