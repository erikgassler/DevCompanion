using System;

namespace DevCompanion.Service
{
	public interface IBlueprint
	{
		Guid Id { get; }
		string Name { get; set; }
	}
}
