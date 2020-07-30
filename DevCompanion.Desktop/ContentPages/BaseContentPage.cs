using System.Windows;
using System.Windows.Controls;

namespace DevCompanion.Desktop.ContentPages
{
	/// <summary>
	/// Base class for Content Page Controls that need an custom event type when Removed so they can unsubscribe from IDesktopService events.
	/// </summary>
	public abstract class BaseContentPage : UserControl
	{
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		public abstract void UnloadForRemoval();
	}
}
