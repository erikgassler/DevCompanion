using Moq;
using System;

namespace DevCompanion.BuildTests
{
	public class MockHelpers
	{
		public T Mock<T>(Action<Mock<T>> setupHandler = null)
			where T: class
		{
			Mock<T> mock = new Mock<T>();
			setupHandler?.Invoke(mock);
			return mock.Object;
		}
	}
}
