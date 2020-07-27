using System.Threading.Tasks;

namespace DevCompanion.Service
{
	public class UnitTypeAzureAppConfig : IUnitTypeAzureAppConfig
	{
		public Constants.UnitType UnitType => Constants.UnitType.AzureAppConfig;

		public Constants.UnitFlag UnitFlag { get; set; } = Constants.UnitFlag.Documentation;

		public Constants.UnitStage UnitState { get; set; } = Constants.UnitStage.Processor;

		public async Task<bool> RunProcessor()
		{
			return false;
		}

		public async Task<bool> RunValidator()
		{
			return false;
		}
	}
}
