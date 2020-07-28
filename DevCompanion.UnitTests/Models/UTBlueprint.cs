using DevCompanion.Service;
using Newtonsoft.Json;
using System;
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
		[Fact]
		public void VerifyBlueprintLifeCycle()
		{
			IBlueprint createdBlueprint = new Blueprint();
			Guid createdId = createdBlueprint.Id;
			Assert.NotEqual(Guid.Empty, createdId);
			createdBlueprint.Name = "Some fantastical name!";
			string json = JsonConvert.SerializeObject(createdBlueprint);
			IBlueprint loadedBlueprint = JsonConvert.DeserializeObject<Blueprint>(json);
			Assert.Equal(createdId, loadedBlueprint.Id);
			Assert.Equal(createdBlueprint.Name, loadedBlueprint.Name);
		}
	}
}
