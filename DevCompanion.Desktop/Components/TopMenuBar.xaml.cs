using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
			Startup.OnServicesReady += Startup_OnServicesReady;
		}

		private void Startup_OnServicesReady(ServiceProvider provider)
		{
			AppSettings = provider.GetService<IAppSettings>();
			DesktopService = provider.GetService<IDesktopService>();
			FileSystem = provider.GetService<IFileSystem>();
			BlueprintLoaderService = provider.GetService<IBlueprintLoaderService>();
			SetupEventHandlers();
			ApplySettings();
		}

		#region Menu Click Handlers
		private void MenuItem_ClickFirstStartupPage(object sender, RoutedEventArgs e)
		{
			DesktopService.ChangeContentPage(Constants.ContentPage.FirstStartup);
		}
		private void AutoSyncBlueprintToggle_Click(object sender, RoutedEventArgs e)
		{
			AppSettings.EnableAutoSyncForCloudBlueprints = this.AutoSyncBlueprintToggle.IsChecked;
		}
		private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
		{
			DesktopService.CloseApplication();
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
			DesktopService.CreateNewBlueprint();
		}

		private void MenuItem_ClickOpenBlueprint(object sender, RoutedEventArgs e)
		{
			DesktopService.ChangeContentPage(Constants.ContentPage.ViewBlueprintList);
		}

		private void MenuItem_ClickSaveBlueprint(object sender, RoutedEventArgs e)
		{
			DesktopService.SaveBlueprint();
		}

		private void MenuItem_ClickSyncBlueprint(object sender, RoutedEventArgs e)
		{
			DesktopService.SyncBlueprint();
		}
		#endregion

		#region Event Handlers
		private void SetupEventHandlers()
		{
			Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;
			DesktopService.OnLoadedBlueprint += DesktopWindow_OnLoadedBlueprint;
		}

		private void DesktopWindow_OnLoadedBlueprint(object sender, IBlueprint blueprint)
		{
			if(blueprint == null)
			{
				MenuItemSaveBlueprint.IsEnabled = false;
				MenuItemSyncBlueprint.IsEnabled = false;
			}
			else
			{
				MenuItemSaveBlueprint.IsEnabled = true;
			}
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
			this.DefaultSaveLocation.ConfigureUpdatingStringSettings(
				() => string.IsNullOrWhiteSpace(AppSettings.DefaultSaveFolder) ? FileSystem.GetFullPath("./Blueprints") : AppSettings.DefaultSaveFolder,
				newValue => AppSettings.DefaultSaveFolder = FileSystem.GetFullPath(newValue)
				);
			this.LocalEncryptionKey.ConfigureUpdatingStringSettings(
				() => AppSettings.LocalEncryptionKey, 
				newValue => AppSettings.LocalEncryptionKey = newValue
				);
			this.CloudStorageAPIEndpoint.ConfigureUpdatingStringSettings(
				() => AppSettings.CloudAPIEndpoint,
				newValue => AppSettings.CloudAPIEndpoint = newValue
				);
			this.CloudStorageAPILicense.ConfigureUpdatingStringSettings(
				() => AppSettings.CloudAPILicense,
				newValue => AppSettings.CloudAPILicense = newValue
				);
			ApplyAutoSyncSetting();
			AppSettings.OnUpdateBlueprintList += (_, list) => LoadBlueprintListItems(list);
			LoadBlueprintListItems(AppSettings.BlueprintList.ToArray());
		}

		private void ApplyAutoSyncSetting()
		{
			this.AutoSyncBlueprintToggle.IsChecked = AppSettings.EnableAutoSyncForCloudBlueprints;
			this.AutoSyncBlueprintToggle.StaysOpenOnClick = true;
			this.AutoSyncBlueprintToggle.Click += AutoSyncBlueprintToggle_Click;
		}
		private void LoadBlueprintListItems(BlueprintRegistryItem[] list)
		{
			// Clear current list
			this.BlueprintList.Items.Clear();
			int addedCount = 0;
			bool showMore = false;
			foreach(BlueprintRegistryItem registryItem in list)
			{
				if(addedCount == 10) {
					showMore = true;
					break;
				}
				addedCount += AddMenuItemForBlueprintRegistryItem(registryItem) ? 1 : 0;
			}
			AddPlaceholderForBlueprintListIfEmpty(addedCount, showMore);
		}

		private bool AddMenuItemForBlueprintRegistryItem(BlueprintRegistryItem registryItem)
		{
			MenuItem menuItem = new MenuItem()
			{
				Header = $"_{registryItem.Name}"
			};
			menuItem.Click += async (object sender, RoutedEventArgs e) =>
			{
				DesktopService.OpenBlueprint(registryItem.Id);
			};
			this.BlueprintList.Items.Add(menuItem);
			return true;
		}

		private void AddPlaceholderForBlueprintListIfEmpty(int addedCount, bool showMore)
		{
			if (showMore)
			{
				MenuItem showMoreButton = new MenuItem()
				{
					Header = "_View All"
				};
				showMoreButton.Click += (_, _) =>
				{
					DesktopService.ChangeContentPage(Constants.ContentPage.ViewBlueprintList);
				};
				this.BlueprintList.Items.Add(showMoreButton);
				return;
			}
			if (addedCount > 0) { return; }
			this.BlueprintList.Items.Add(new MenuItem()
			{
				Header = "_No Blueprints Found!"
			});
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
		private IFileSystem FileSystem { get; set; }
		private IDesktopService DesktopService { get; set; }
		private IAppSettings AppSettings { get; set; }
		private IBlueprintLoaderService BlueprintLoaderService { get; set; }
		#endregion
	}
}
