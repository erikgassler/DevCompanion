using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitToolbar.xaml
	/// </summary>
	public partial class ControlUnitToolbar : UserControl
	{
		public ControlUnitToolbar()
		{
			InitializeComponent();
		}

		private void CongrolDrag_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton != MouseButtonState.Pressed)
			{
				return;
			}
		}

		public event EventHandler<int> OnClickDelete;
		private void ControlDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OnClickDelete?.Invoke(this, 1);
		}
	}
}
