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

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageBlueprintList.xaml
	/// </summary>
	public partial class PageBlueprintList : UserControl
	{
		public PageBlueprintList(
			IDesktopService desktopService,
			IAppSettings appSettings
			)
		{
			DesktopService = desktopService;
			AppSettings = appSettings;
			InitializeComponent();
			PopulateListForBlueprints();
		}

		private void PopulateListForBlueprints()
		{
			foreach(IBlueprintRegistryItem item in AppSettings.BlueprintList)
			{
				AddButtonForRegistryItem(item);
			}
		}

		private void AddButtonForRegistryItem(IBlueprintRegistryItem item)
		{
			Button button = new Button()
			{
				Padding = new Thickness(20),
				Margin = new Thickness(0, 0, 0, 10),
				Content = $"{item.Name} ({item.FilePath}) [{item.Key}]"
			};
			button.Click += (sender, e) =>
			{
				DesktopService.OpenBlueprint(item.Id);
			};
			ControlBlueprintList.Children.Add(button);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}

		IDesktopService DesktopService { get; }
		IAppSettings AppSettings { get; }
	}
}
