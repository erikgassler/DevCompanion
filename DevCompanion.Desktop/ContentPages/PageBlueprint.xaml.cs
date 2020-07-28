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

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageBlueprint.xaml
	/// </summary>
	public partial class PageBlueprint : UserControl
	{
		public PageBlueprint(
			ServiceProvider provider,
			IBlueprint blueprint
			)
		{
			Provider = provider;
			DesktopWindow = provider.GetService<IDesktopWindow>();
			InitializeComponent();
			DesktopWindow.UpdateStatus($"Blueprint Loaded!");
		}

		private IDesktopWindow DesktopWindow { get; }
		private ServiceProvider Provider { get; }
	}
}
