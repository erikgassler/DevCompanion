using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Interaction logic for PageError.xaml
	/// </summary>
	public partial class PageError : UserControl
	{
		public PageError()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public string Message { get => ErrorMessage.Text ?? "An unexpected error occurred"; set => ErrorMessage.Text = value; }
	}
}
