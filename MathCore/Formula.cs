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
			: base(new byte[variables.Count].Select(s => typeof(KeyValuePair<string, double>)).ToArray(),
				  new Type[] { typeof(double) },  (s) => new object[] {
				  @delegate(((IReadOnlyDictionary<string, double>)s).ToDictionary(m => m.Key, m => m.Value)) })
		{
			this.variables = variables;
		}


		protected override void OnInvoking(IReadOnlyList<object> args)
		{
			var a = (IReadOnlyDictionary<string, double>)args;
			foreach (var item in a)
			{
				var var = variables.Single(s => s.Name == item.Key);
				if(!CheckConstrains(var, item.Value))
					throw new ArgumentException("One or more variables has invalid value");
			}
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

		public double Invoke(IReadOnlyDictionary<string, double> args) =>
			(double)Invoke(args.ToList()).Single();

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


		public enum NumberConstrain
		{
			None		= 0b0000_0000,
			Integer		= 0b1000_0000,
			Even		= 0b1001_0000,
			Odd			= 0b1000_1000,
			NoNegative	= 0b0100_0000,
			NoZero		= 0b0010_0000,

			//Auto implements
			Positive	= 0b0110_0000
		}

		public struct Variable
		{
			public string Name { get; set; }

			public NumberConstrain Constrains { get; set; }
		}
	}
}
