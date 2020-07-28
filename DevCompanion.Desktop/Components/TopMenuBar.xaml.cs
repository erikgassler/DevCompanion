﻿using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
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
			DesktopWindow = provider.GetService<IDesktopWindow>();
			SetupEventHandlers();
			ApplySettings();
		}

		#region Menu Click Handlers
		private void MenuItem_ClickFirstStartupPage(object sender, RoutedEventArgs e)
		{
			DesktopWindow.ChangeContentPage(Constants.ContentPage.FirstStartup);
		}
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
			DesktopService.CreateNewBlueprint();
		}

		private void MenuItem_ClickOpenBlueprint(object sender, RoutedEventArgs e)
		{
			DesktopService.OpenBlueprint();
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
			DesktopWindow.OnLoadedBlueprint += DesktopWindow_OnLoadedBlueprint;
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
			ConfigureUpdatingStringSettings(
				this.LocalEncryptionKey, 
				() => AppSettings.LocalEncryptionKey, 
				newValue => AppSettings.LocalEncryptionKey = newValue
				);
			ConfigureUpdatingStringSettings(
				this.CloudStorageAPIEndpoint,
				() => AppSettings.CloudAPIEndpoint,
				newValue => AppSettings.CloudAPIEndpoint = newValue
				);
			ConfigureUpdatingStringSettings(
				this.CloudStorageAPILicense,
				() => AppSettings.CloudAPILicense,
				newValue => AppSettings.CloudAPILicense = newValue
				);
			ApplyAutoSyncSetting();
			LoadBlueprintListItems();
		}

		private void ConfigureUpdatingStringSettings(MenuItemTextInput input, Func<string> onLoad, Action<string> onSave)
		{
			input.RefValue = onLoad();
			input.RefValueUpdated += (object sender, EventArgs e) => onSave(input.RefValue);
			input.OnOpeningEditor += (object sender, TextBox field) => input.RefValue = onLoad();
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
		private IDesktopService DesktopService { get; set; }
		private IDesktopWindow DesktopWindow { get; set; }
		private IAppSettings AppSettings { get; set; }
		#endregion
	}
}
