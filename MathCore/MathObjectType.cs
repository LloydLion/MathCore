using System;

namespace MathCore
{
	public abstract class MathObjectType
	{
		public abstract object GetDefaultInterface();

		public abstract OperationFactory[] GetDefaultOperations();
	}
}