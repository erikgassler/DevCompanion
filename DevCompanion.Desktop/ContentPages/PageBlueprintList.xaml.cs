using DevCompanion.Service;
using System.Windows;
using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageBlueprintList.xaml
	/// </summary>
	public partial class PageBlueprintList : BaseContentPage
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

		public override void UnloadForRemoval()
		{
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
