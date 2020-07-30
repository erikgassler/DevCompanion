using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageError.xaml
	/// </summary>
	public partial class PageError : BaseContentPage
	{
		public PageError()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public override void UnloadForRemoval()
		{
		}

		public string Message { get => ErrorMessage.Text ?? "An unexpected error occurred"; set => ErrorMessage.Text = value; }
	}
}
