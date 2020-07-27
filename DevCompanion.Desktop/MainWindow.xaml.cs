using DevCompanion.Desktop.StaticContent;
using DevCompanion.Service;
using DevCompanion.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevCompanion.Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IDisposable
	{
		private bool disposedValue;

		public MainWindow()
		{
			InitializeComponent();
			DesktopService = Startup.GetService<IDesktopService>();
			AttachStartupContent();
			CustomToolbar.MouseDown += TopMenuBar_MouseDown;
			Uri imagePath = new Uri("Logo_Watermark.png", UriKind.Relative);
			BackgroundImage.ImageSource = new BitmapImage(imagePath);
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
			this.LatestStatusUpdate.Text = status;
		}

		private void AttachStartupContent()
		{
			FirstStartup startup = new FirstStartup();
			MainContentContainer.Children.Add(startup);
		}


		private IDesktopService DesktopService { get; set; }

		#region IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
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
