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

namespace DevCompanion.Desktop.Components
{
	/// <summary>
	/// Interaction logic for MenuItemTextInput.xaml
	/// </summary>
	public partial class MenuItemTextInput : UserControl
	{
		public MenuItemTextInput()
		{
			InitializeComponent();
			this.DataContext = this;
			this.OpenEditor.Click += OpenEditor_Click;
			this.CancelUpdate.Click += CancelUpdate_Click;
			this.SaveChange.Click += SaveChange_Click;
			DisableEditing();
		}

		private void EnableEditing()
		{
			OpenEditor.Visibility = Visibility.Collapsed;
			TextValue.Visibility = Visibility.Visible;
			CancelUpdate.Visibility = Visibility.Visible;
			SaveChange.Visibility = Visibility.Visible;
		}

		private void DisableEditing()
		{
			TextValue.Visibility = Visibility.Collapsed;
			CancelUpdate.Visibility = Visibility.Collapsed;
			SaveChange.Visibility = Visibility.Collapsed;
			OpenEditor.Visibility = Visibility.Visible;
		}

		private void SaveChange_Click(object sender, RoutedEventArgs e)
		{
			RefValue = TextValue.Text.Trim();
			RefValueUpdated?.Invoke(this, e);
			DisableEditing();
		}

		private void CancelUpdate_Click(object sender, RoutedEventArgs e)
		{
			DisableEditing();
		}

		private void OpenEditor_Click(object sender, RoutedEventArgs e)
		{
			OnOpeningEditor?.Invoke(this, TextValue);
			TextValue.Text = RefValue;
			EnableEditing();
		}

		public event EventHandler RefValueUpdated;
		public event EventHandler<TextBox> OnOpeningEditor;
		public string RefValue { get; set; }
		public string DisplayText { get => (string)OpenEditor.Content; set => OpenEditor.Content = value; }
	}
}
