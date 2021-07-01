using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public abstract class MathObjectPresentation<T> : MathObjectPresentation where T : MathObjectType, new()
	{
		protected List<OperationFactory> Operations { get; } = new List<OperationFactory>();


		public MathObjectPresentation()
		{
			Operations.AddRange(Type.GetDefaultOperations());
		}


		public override Type MathObjectType => typeof(T);

		public MathObjectType Type { get; } = new T();


		public Operation<TOut, TIn1, TIn2> GetOperation<TOut, TIn1, TIn2>(string token)
			where TOut : MathObjectType, new()
			where TIn1 : MathObjectType, new()
			where TIn2 : MathObjectType, new()
		{
			return Operations.Single(s => s.Token == token).CastType<TOut, TIn1, TIn2>().CreateOperation();
		}

		public void AddOperationFactory(OperationFactory operationFactory)
		{
			Operations.Add(operationFactory);
		}
	}

	public abstract class MathObjectPresentation
	{
		public abstract object GetInterfaceObject();

		public abstract Type MathObjectType { get; }
	}
}
