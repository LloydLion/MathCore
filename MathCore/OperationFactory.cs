using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public abstract class OperationFactory<TOut, TIn1, TIn2> : OperationFactory
		where TOut : MathObjectType, new()
		where TIn1 : MathObjectType, new()
		where TIn2 : MathObjectType, new()
	{
		public abstract OperationDelegate<TOut, TIn1, TIn2> OperationDelegate { get; }

		public override string Key { get; }

		public override string Token { get; }


		public Operation<TOut, TIn1, TIn2> CreateOperation()
		{
			return new Operation<TOut, TIn1, TIn2>(OperationDelegate);
		}


		public OperationFactory(string key, string token)
		{
			Key = key;
			Token = token;
		}
	}

	public abstract class OperationFactory
	{
		public abstract string Key { get; }
		
		public abstract string Token { get; }


		public OperationFactory<TOut, TIn1, TIn2> CastType<TOut, TIn1, TIn2>()
			where TOut : MathObjectType, new()
			where TIn1 : MathObjectType, new()
			where TIn2 : MathObjectType, new()
		{
			return (OperationFactory<TOut, TIn1, TIn2>)this;
		}
	}
}
