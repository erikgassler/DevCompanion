using DevCompanion.Desktop.StaticContent;
using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
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
		private bool disposedValue;

		public MainWindow()
		{
			Startup.ClientServices = services =>
			{
				services.Replace(new ServiceDescriptor(typeof(IFileSystem), typeof(FileSystem), ServiceLifetime.Singleton));
				services.Replace(new ServiceDescriptor(typeof(IDesktopWindow), this));
			};
			InitializeComponent();
			UpdateProcessingProgress(-1);
			CustomToolbar.MouseDown += TopMenuBar_MouseDown;
			Uri imagePath = new Uri("Logo_Watermark.png", UriKind.Relative);
			BackgroundImage.ImageSource = new BitmapImage(imagePath);
			DesktopService = Startup.GetService<IDesktopService>();
			Startup.DesktopServicesAreReady();
		}
		#region Toolbar Dragging

		private void TopMenuBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton != MouseButtonState.Pressed)
			{
				return;
			}
			this.DragMove();
		}


		#endregion

		public void UpdateStatus(string status)
		{
			this.Dispatcher.Invoke(() =>
			{
				this.LatestStatusUpdate.Text = status;
			});
		}

		/// <summary>
		/// set with a range from 0 to 100 to display.
		/// Set to -1 to hide.
		/// </summary>
		/// <param name="percent"></param>
		public void UpdateProcessingProgress(int percent)
		{
			this.Dispatcher.Invoke(() =>
			{
				ProcessingProgress.Visibility = (percent < 0 || percent > 100) ? Visibility.Collapsed : Visibility.Visible;
				ProcessingProgress.Value = percent;
			});
		}

		public void ChangeContentPage(Constants.ContentPage page)
		{
			lock (LockPageSwap)
			{
				if (page == CurrentPage) { return; }
				CurrentPage = page;
				if (CurrentPageControl != null)
				{
					MainContentContainer.Children.Remove(CurrentPageControl);
				}
				switch (page)
				{
					case Constants.ContentPage.FirstStartup:
					default:
						CurrentPage = Constants.ContentPage.FirstStartup;
						CurrentPageControl = new FirstStartup();
						break;
				}
				MainContentContainer.Children.Add(CurrentPageControl);
			}
		}

		public void LoadBlueprint(IBlueprint blueprint)
		{

		}

		public void OpenBlueprint()
		{

		}

		public void SaveBlueprint()
		{

		}

		public void SyncBlueprint()
		{

		}

		public void CloseApplication()
		{
			DesktopService?.StopDesktopServices();
			Startup.CloseServices();
			this.Close();
		}

		private object LockPageSwap { get; } = new object();
		private Constants.ContentPage CurrentPage { get; set; }
		private UserControl CurrentPageControl { get; set; }
		private IDesktopService DesktopService { get; set; }

		#region IDisposable
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
