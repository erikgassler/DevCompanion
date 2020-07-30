using DevCompanion.Service;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DevCompanion.BuildTests.Models
{
	public class UTBlueprint : MockHelpers
	{
		/// <summary>
		/// Want to test the cycle of:
		/// - creating a new blueprint
		/// - converting blueprint to JSON, which is done as part of saving
		/// - converting JSON back to a new blureprint object
		/// - verify no data was lost in conversion
		/// </summary>
		[Theory]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Processor, Constants.UnitType.AzureAppConfig)]
		[InlineData(Constants.UnitFlag.Deployment, Constants.UnitStage.Processor, Constants.UnitType.AzureAppConfig)]
		[InlineData(Constants.UnitFlag.Documentation, Constants.UnitStage.Processor, Constants.UnitType.AzureAppConfig)]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Validator, Constants.UnitType.AzureAppConfig)]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Processor, Constants.UnitType.AzureKeyVault)]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Processor, Constants.UnitType.Blueprint)]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Processor, Constants.UnitType.CommandPromptScript)]
		[InlineData(Constants.UnitFlag.Configuration, Constants.UnitStage.Processor, Constants.UnitType.Workflow)]
		public async Task VerifyBlueprintLifeCycle(Constants.UnitFlag flag, Constants.UnitStage stage, Constants.UnitType type)
		{
			IBlueprint createdBlueprint = new Blueprint();
			createdBlueprint.Units.Add(new BaseBlueprintUnit()
			{
				UnitFlag = flag,
				UnitState = stage,
				UnitType = type
			});
			Guid createdId = createdBlueprint.Id;
			Assert.NotEqual(Guid.Empty, createdId);
			createdBlueprint.Name = "Some fantastical name!";
			string json = JsonConvert.SerializeObject(createdBlueprint);
			IBlueprint loadedBlueprint = JsonConvert.DeserializeObject<Blueprint>(json);
			Assert.Equal(createdId, loadedBlueprint.Id);
			Assert.Equal(createdBlueprint.Name, loadedBlueprint.Name);
			Assert.Equal(flag, loadedBlueprint.Units[0].UnitFlag);
			Assert.Equal(stage, loadedBlueprint.Units[0].UnitState);
			Assert.Equal(type, loadedBlueprint.Units[0].UnitType);
			Assert.False(await loadedBlueprint.Units[0].RunProcessor());
			Assert.False(await loadedBlueprint.Units[0].RunValidator());
		}
	}
}
