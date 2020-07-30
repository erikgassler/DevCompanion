using System;
using System.Windows;
using System.Windows.Controls;

namespace DevCompanion.Desktop.Components
{
	/// <summary>
	/// Interaction logic for MenuItemTextInput.xaml
	/// </summary>
	public partial class EditableTextDisplay : UserControl
	{
		public EditableTextDisplay()
		{
			InitializeComponent();
			this.DataContext = this;
			this.OpenEditor.Click += OpenEditor_Click;
			this.ButtonCancelUpdate.Click += CancelUpdate_Click;
			this.ButtonSaveChange.Click += SaveChange_Click;
			this.ButtonEdit.Click += OpenEditor_Click;
			DisableEditing();
		}

		public void ConfigureUpdatingStringSettings(Func<string> onLoad, Action<string> onSave)
		{
			this.RefValue = onLoad();
			this.RefValueUpdated += (object sender, EventArgs e) => onSave(this.RefValue);
			this.OnOpeningEditor += (object sender, TextBox field) => this.RefValue = onLoad();
		}

		private void EnableEditing()
		{
			OpenEditor.Visibility = Visibility.Collapsed;
			ButtonEdit.Visibility = Visibility.Collapsed;
			TextValue.Visibility = Visibility.Visible;
			ButtonCancelUpdate.Visibility = Visibility.Visible;
			ButtonSaveChange.Visibility = Visibility.Visible;
		}

		private void DisableEditing()
		{
			TextValue.Visibility = Visibility.Collapsed;
			ButtonCancelUpdate.Visibility = Visibility.Collapsed;
			ButtonSaveChange.Visibility = Visibility.Collapsed;
			OpenEditor.Visibility = Visibility.Visible;
			ButtonEdit.Visibility = Visibility.Visible;
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
		public int MaxLines { get => TextValue.MaxLines; set => TextValue.MaxLines = value; }
		public int MinLines { get => TextValue.MinLines; set => TextValue.MinLines = value; }
	}
}
