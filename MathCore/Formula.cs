using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Formula : Function
	{
		public IReadOnlyCollection<Variable> Variables => variables;


		private readonly IReadOnlyCollection<Variable> variables;


		public Formula(IReadOnlyCollection<Variable> variables, Func<IReadOnlyDictionary<string, double>, double> @delegate)
			: base(new Type[] { typeof(IReadOnlyDictionary<string, double>) },
				  new Type[] { typeof(double) },  (s) => new object[] {
				  @delegate((IReadOnlyDictionary<string, double>)s[0]) })
		{
			this.variables = variables;
		}


		protected override void OnInvoking(IReadOnlyList<object> args)
		{
			var a = (IReadOnlyDictionary<string, double>)args[0];
			if(!IsOkArgs(a))
				throw new ArgumentException("One or more variables has invalid value");
		}

		public bool IsOkArgs(IReadOnlyDictionary<string, double> args)
		{
			foreach (var arg in args)
			{
				var var = variables.Single(s => s.Name == arg.Key);
				if (!CheckConstrains(var, arg.Value)) return false;
			}

			return true;
		}

		public double CalculateFormula(IReadOnlyDictionary<string, double> args) =>
			(double)InvokeSingle(args);

		private bool CheckConstrains(Variable var, double val)
		{
			if (var.Constrains.HasFlag(NumberConstrain.Integer))
				if (val != Math.Floor(val))
					return false;

			if (var.Constrains.HasFlag(NumberConstrain.NoNegative))
				if (val < 0)
					return false;

			if (var.Constrains.HasFlag(NumberConstrain.NoZero))
				if (val == 0)
					return false;

			if (var.Constrains.HasFlag(NumberConstrain.Even)) //integer has checked
				if (val % 2 == 1) return false;

			if (var.Constrains.HasFlag(NumberConstrain.Odd)) //integer has checked
				if (val % 2 == 0) return false;

			return true;
		}


		public struct Variable
		{
			public string Name { get; set; }

			public NumberConstrain Constrains { get; set; }
		}
	}
}
