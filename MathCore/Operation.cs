using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public delegate MathObjectPresentation<TOut> OperationDelegate<TOut, TIn1, TIn2>(MathObjectPresentation<TIn1> param1, MathObjectPresentation<TIn2> param2)
		where TOut : MathObjectType, new()
		where TIn1 : MathObjectType, new()
		where TIn2 : MathObjectType, new();


	public class Operation<TOut, TIn1, TIn2> : MathObjectPresentation<TOut> 
		where TOut : MathObjectType, new() 
		where TIn1 : MathObjectType, new()
		where TIn2 : MathObjectType, new()
	{
		private MathObjectPresentation<TOut> result;


		public OperationDelegate<TOut, TIn1, TIn2> OperationDelegate { get; }


		public Operation(OperationDelegate<TOut, TIn1, TIn2> operationDelegate)
		{
			OperationDelegate = operationDelegate;
		}


		public override object GetInterfaceObject()
		{
			if (result == null)
				throw new InvalidOperationException
					("MathObjectPresentation<T> is Operation<TOut> and can't give interface before calculating");
			else return result.GetInterfaceObject();
		}

		public Operation<TOut, TIn1, TIn2> Calculate(MathObjectPresentation<TIn1> param1, MathObjectPresentation<TIn2> param2)
		{
			result = OperationDelegate(param1, param2);
			return this;
		}
	}
}
