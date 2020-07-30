using DevCompanion.Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageCreateBlueprint.xaml
	/// </summary>
	public partial class PageCreateBlueprint : BaseContentPage
	{
		public PageCreateBlueprint(
			IDesktopService desktopService,
			IFileSystem fileSystem
			)
		{
			DesktopService = desktopService;
			Storage = desktopService.ActiveBlueprintStorage;
			Blueprint = Storage.Blueprint;
			FileSystem = fileSystem;
			InitializeComponent();
			SetupPage();
		}

		public override void UnloadForRemoval()
		{
		}

		private void SetupPage()
		{
			SetupBlueprintName();
			SetupBlueprintKey();
			SetupFileLocation();
		}

		private void ControlSaveBlueprint_Click(object sender, RoutedEventArgs e)
		{
			SetFilePath(FileLocation.Text);
			FileLocation.Text = GetFolderFromPath(Storage.BlueprintRegistryItem.FilePath);
			Blueprint.Name = BlueprintName.Text;
			DesktopService.SaveBlueprint();
		}

		private void SetupBlueprintName()
		{
			BlueprintName.Text = Blueprint.Name;
		}

		private void SetupBlueprintKey()
		{
			Storage.BlueprintRegistryItem.Key = Guid.NewGuid().ToString();
			BlueprintKey.Text = Storage.BlueprintRegistryItem.Key;
		}

		private void SetupFileLocation()
		{
			FileLocation.Text = GetFolderFromPath(Storage.BlueprintRegistryItem.FilePath);
		}

		private void SetFilePath(string folderPath)
		{
			if (!FileSystem.IsValidDirectory(folderPath)) { return; }
			Storage.BlueprintRegistryItem.SetFilePath(folderPath);
		}

		private string GetFolderFromPath(string path)
		{
			return FileSystem.GetDirectoryName(path);
		}

		private IFileSystem FileSystem { get; }
		private IBlueprintStorage Storage { get; }
		private IBlueprint Blueprint { get; }
		private IDesktopService DesktopService { get; }
	}
}
