using DevCompanion.Desktop.ContentPages;
using DevCompanion.Desktop.StaticContent;
using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DevCompanion.Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IDesktopWindow, IDisposable
	{
		public MainWindow()
		{
			Startup.ClientServices = services =>
			{
				services.Replace(new ServiceDescriptor(typeof(IFileSystem), typeof(FileSystem), ServiceLifetime.Singleton));
				services.Replace(new ServiceDescriptor(typeof(IDesktopWindow), this));
			};
			InitializeComponent();
			CustomToolbar.MouseDown += TopMenuBar_MouseDown;
			Uri imagePath = new Uri("Logo_Watermark.png", UriKind.Relative);
			BackgroundImage.ImageSource = new BitmapImage(imagePath);
			Provider = Startup.GetProvider();
			DesktopService = Provider.GetService<IDesktopService>();
			AppSettings = Provider.GetService<IAppSettings>();
			FileSystem = Provider.GetService<IFileSystem>();
			SubscribeToServiceEvents();
			Startup.DesktopServicesAreReady();
			DesktopService.RunStartup();
		}
		#region Toolbar Dragging

		private void SubscribeToServiceEvents()
		{
			DesktopService.OnCloseApplication += (sender, _) =>
			{
				this.Dispatcher.Invoke(() =>
				{
					this.CloseApplication();
				});
			};
			DesktopService.OnStatusUpdated += (sender, status) =>
			{
				this.Dispatcher.Invoke(() =>
				{
					this.LatestStatusUpdate.Text = status;
				});
			};
			DesktopService.OnProgessUpdated += (sender, percent) =>
			{
				this.Dispatcher.Invoke(() =>
				{
					ProcessingProgress.Visibility = (percent < 0 || percent > 100) ? Visibility.Collapsed : Visibility.Visible;
					ProcessingProgress.Value = percent;
				});
			};
			DesktopService.OnChangeContentPage += (sender, page) =>
			{
				this.Dispatcher.Invoke(() =>
				{
					if (CurrentPageControl != null)
					{
						CurrentPageControl.UnloadForRemoval();
						MainContentContainer.Children.Remove(CurrentPageControl);
					}
					CurrentPageControl = page switch
					{
						Constants.ContentPage.Loading => new PageLoading(),
						Constants.ContentPage.CreateBlueprint => new PageCreateBlueprint(DesktopService, FileSystem),
						Constants.ContentPage.ViewBlueprintList => new PageBlueprintList(DesktopService, AppSettings),
						Constants.ContentPage.ViewBlueprint => new PageBlueprint(DesktopService),
						Constants.ContentPage.FirstStartup => new PageFirstStartup(Provider),
						_ => new PageError() { Message = "Sorry! We didn't finish setting up this component yet." },
					};
					MainContentContainer.Children.Add(CurrentPageControl);
				});
			};
		}

		private void TopMenuBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton != MouseButtonState.Pressed)
			{
				return;
			}
			this.DragMove();
		}
		#endregion

		private void CloseApplication()
		{
			Startup.CloseServices();
			this.Close();
		}

		private BaseContentPage CurrentPageControl { get; set; }
		private ServiceProvider Provider { get; set; }
		private IDesktopService DesktopService { get; set; }
		private IFileSystem FileSystem { get; set; }
		private IAppSettings AppSettings { get; set; }

		#region IDisposable
		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					DesktopService?.StopDesktopServices();
					Startup.CloseServices();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~MainWindow()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
