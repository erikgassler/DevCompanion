using DevCompanion.Service;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace DevCompanion.Desktop.UnitTypes
{
	/// <summary>
	/// Interaction logic for ControlUnitTypeDocumentation.xaml
	/// </summary>
	public partial class ControlUnitTypeDocumentation : ControlBaseUnit
	{
		public ControlUnitTypeDocumentation(IBlueprintUnit unit)
		{
			BlueprintUnit = unit;
			InitializeComponent();
			SetupControl();
		}

		private void SetupControl()
		{
			ControlToolbar.OnClickDelete += ControlToolbar_OnClickDelete;
			LoadContentIntoControl();
			ControlMainContent.TextChanged += ControlMainContent_TextChanged;
		}

		private void ControlToolbar_OnClickDelete(object sender, int e)
		{
			if(ControlMainContent.Document.Blocks.Count > 0)
			{
				ControlMainContent.Document.Blocks.Clear();
				return;
			}
			// TODO - Remove control from list of units
		}

		private void ControlMainContent_TextChanged(object sender, TextChangedEventArgs e)
		{
			SaveContentFromControl();
		}

		private void LoadContentIntoControl()
		{
			if (string.IsNullOrWhiteSpace(BlueprintUnit.DataContent)) { return; }
			using StringReader stringReader = new StringReader(BlueprintUnit.DataContent);
			using System.Xml.XmlReader reader = System.Xml.XmlReader.Create(stringReader);
			FlowDocument flowDocument = XamlReader.Load(reader) as FlowDocument;
			ControlMainContent.Document = flowDocument;
		}

		private void SaveContentFromControl()
		{
			FlowDocument flowDocument = ControlMainContent.Document;
			using StringWriter stringwriter = new StringWriter();
			using System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(stringwriter);
			XamlWriter.Save(flowDocument, writer);
			string toSave = stringwriter.ToString();
			BlueprintUnit.DataContent = toSave;
		}

		private IBlueprintUnit BlueprintUnit { get; }
	}
}
