using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Function
	{
		private readonly Func<IReadOnlyList<object>, IReadOnlyList<object>> @delegate;


		public Function(Type[] inputTypes, Type[] outputTypes,
			Func<IReadOnlyList<object>, IReadOnlyList<object>> @delegate)
		{
			InputTypes = inputTypes;
			OutputTypes = outputTypes;
			this.@delegate = @delegate;
		}


		public IReadOnlyList<Type> InputTypes { get; }

		public IReadOnlyList<Type> OutputTypes { get; }


		public IReadOnlyList<object> Invoke(IReadOnlyList<object> args)
		{
			if (args.Count != InputTypes.Count)
				throw new ArgumentException("Args count isn't equals exepted Inputs count", nameof(args));

			for (int i = 0; i < args.Count; i++)
				if (InputTypes[i].IsAssignableFrom(args[i].GetType()))
					throw new ArgumentException($"Argument at {i} index has unexepted type");

			OnInvoking(args);
			var ret = @delegate(args);

			for (int i = 0; i < ret.Count; i++)
				if (OutputTypes[i].IsAssignableFrom(ret[i].GetType()))
					throw new Exception($"Invalid function delegate. Result at {i} index has unexepted type");

			if (ret.Count != OutputTypes.Count)
				throw new Exception("Invalid function delegate. Count of given results isn't equals exepted Outputs count");

			OnInvoked(args, ret);
			return ret;
		}

		public IReadOnlyList<object> Invoke(params object[] args) => Invoke(args);

		public object InvokeSingle(params object[] args) => Invoke(args).Single();

		public object InvokeSingle(IReadOnlyList<object> args) => Invoke(args).Single();

		protected virtual void OnInvoking(IReadOnlyList<object> args) { }

		protected virtual void OnInvoked(IReadOnlyList<object> args, IReadOnlyList<object> ret) { }
	}
}
