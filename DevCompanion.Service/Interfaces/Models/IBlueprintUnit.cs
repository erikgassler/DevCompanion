using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public interface IBlueprintUnit
	{
		Constants.UnitType UnitType { get; }
		Constants.UnitFlag UnitFlag { get; }
		Constants.UnitStage UnitState { get; }

		/// <summary>
		/// All data for Units are stored in this field either as raw text (e.g. documentation) or JSON data.
		/// </summary>
		string DataContent { get; set; }

		Task<bool> RunProcessor();

		Task<bool> RunValidator();
	}
}
