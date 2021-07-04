using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore.Objects
{
	public class Function : MathObject
	{
		public int Outputs { get; private set; }

		public Func<MathObject[], MathObject[]> FunctionDelegate { get; private set; }

		public int Inputs { get; private set; }


		public Function InitObject(int inputs, int outputs, Func<MathObject[], MathObject[]> fnDelegate)
		{
			Inputs = inputs;
			Outputs = outputs;
			FunctionDelegate = fnDelegate;
			return this;
		}

		public MathObject[] Calculate(MathObject[] objects) => FunctionDelegate(objects);

		public MathObject CalculateSingle(params MathObject[] objects) => FunctionDelegate(objects).Single();
	}
}
