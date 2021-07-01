using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public class DelegateOperationFactory<TOut, TIn1, TIn2> : OperationFactory<TOut, TIn1, TIn2>
		where TOut : MathObjectType, new()
		where TIn1 : MathObjectType, new()
		where TIn2 : MathObjectType, new()
	{
		public DelegateOperationFactory(OperationDelegate<TOut, TIn1, TIn2> operationDelegate, string key, string token) : base(key, token)
		{
			OperationDelegate = operationDelegate;
		}


		public override OperationDelegate<TOut, TIn1, TIn2> OperationDelegate { get; }
	}
}
