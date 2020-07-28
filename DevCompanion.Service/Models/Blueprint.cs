using System;

namespace DevCompanion.Service
{
	public class Blueprint : IBlueprint
	{
		public Blueprint()
		{
			Id = Guid.NewGuid();
			Name = "New Blueprint";
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
