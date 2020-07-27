using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public interface IBlueprintUnit
	{
		Constants.UnitType UnitType { get; }
		Constants.UnitFlag UnitFlag { get; }
		Constants.UnitStage UnitState { get; }

		Task<bool> RunProcessor();

		Task<bool> RunValidator();
	}
}
