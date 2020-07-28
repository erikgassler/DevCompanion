using DevCompanion.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DevCompanion.BuildTests.UnitTypes
{
	public class ITUnitTypes
	{
		[Theory]
		[InlineData(Constants.UnitType.AzureAppConfig, typeof(IUnitTypeAzureAppConfig))]
		[InlineData(Constants.UnitType.AzureKeyVault, typeof(IUnitTypeAzureKeyVault))]
		[InlineData(Constants.UnitType.Blueprint, typeof(IUnitTypeBlueprint))]
		[InlineData(Constants.UnitType.CommandPromptScript, typeof(IUnitTypeCommandPromptScript))]
		[InlineData(Constants.UnitType.Documentation, typeof(IUnitTypeDocumentation))]
		[InlineData(Constants.UnitType.EnvironmentVariable, typeof(IUnitTypeEnvironmentVariable))]
		[InlineData(Constants.UnitType.PowerShellScript, typeof(IUnitTypePowerShellScript))]
		[InlineData(Constants.UnitType.Workflow, typeof(IUnitTypeWorkflow))]
		public void VerifyInstances(Constants.UnitType unitType, Type type)
		{
			var dataType = new Type[] { type };
			var genericBase = typeof(MockInstance<>);
			var combinedType = genericBase.MakeGenericType(dataType);
			dynamic instance = Activator.CreateInstance(combinedType);
			dynamic service = instance.GetService();
			Assert.Equal(unitType, service.UnitType);
		}

		private class MockInstance<T>
			where T: IBlueprintUnit
		{
			public T GetService()
			{
				ServiceProvider provider = Startup.GetProvider();
				return provider.GetService<T>();
			}
		}
	}
}
