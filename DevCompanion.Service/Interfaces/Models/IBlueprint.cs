using System;
using System.Collections.Generic;

namespace DevCompanion.Service
{
	public interface IBlueprint
	{
		Guid Id { get; }
		string Name { get; set; }
		IList<BaseBlueprintUnit> Units { get; }
	}
}
