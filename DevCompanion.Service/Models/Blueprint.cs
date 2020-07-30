using System;
using System.Collections.Generic;

namespace DevCompanion.Service
{
	public class Blueprint : IBlueprint
	{
		public Blueprint()
		{
			Id = Guid.NewGuid();
			Name = "New Blueprint";
			Units = new List<BaseBlueprintUnit>();
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public IList<BaseBlueprintUnit> Units { get; }
	}
}
