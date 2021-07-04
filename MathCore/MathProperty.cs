using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public class MathProperty<T> : MathProperty where T : MathObject
	{
		public override Type StoreType => typeof(T);
	}

	public abstract class MathProperty
	{
		public abstract Type StoreType { get; }
	}
}
