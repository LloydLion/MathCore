using MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	[TestClass]
	public class FormulaTest
	{
		[TestMethod]
		public void BasicTest()
		{
			var formula = new Formula(new Formula.Variable[]
			{
				new Formula.Variable() { Name = "x", Constrains = NumberConstrain.None },
				new Formula.Variable() { Name = "y", Constrains = NumberConstrain.NoZero },
				new Formula.Variable() { Name = "k", Constrains = NumberConstrain.Positive | NumberConstrain.Integer }

			}, TestDelegate);


			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.NextDouble() * (random.Next(10000) - 5000);

				double y = 0;
				while(y == 0) y = random.NextDouble() * (random.Next(10000) - 5000);

				var k = random.Next(5000 - 1) + 1;

				Assert.AreEqual(formula.CalculateFormula(new Dictionary<string, double>() { ["x"] = x, ["y"] = y, ["k"] = k }), x + k*y);
			}
		}

		[TestMethod]
		public void Constrains()
		{
			var formula = new Formula(new Formula.Variable[]
			{
				new Formula.Variable() { Name = "x", Constrains = NumberConstrain.None },
				new Formula.Variable() { Name = "y", Constrains = NumberConstrain.NoZero },
				new Formula.Variable() { Name = "k", Constrains = NumberConstrain.Positive },

				new Formula.Variable() { Name = "m1", Constrains = NumberConstrain.Even },
				new Formula.Variable() { Name = "m2", Constrains = NumberConstrain.NoNegative },
				new Formula.Variable() { Name = "m3", Constrains = NumberConstrain.Odd },
				new Formula.Variable() { Name = "m4", Constrains = NumberConstrain.Integer },

			}, TestDelegate);


			var args = new Dictionary<string, double>()
			{
				["x"] = 4.123,
				["y"] = 8.231,
				["k"] = 9.4191,
				["m1"] = 6,
				["m2"] = 0,
				["m3"] = 9,
				["m4"] = -3578,
			};

			formula.CalculateFormula(args);

			//------------------------------------------------

			var tmp = args["y"];
			args["y"] = 0;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["y"] = tmp;

			tmp = args["k"];
			args["k"] = -3.123;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["k"] = 0;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["k"] = tmp;

			tmp = args["m1"];
			args["m1"] = 1;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["m1"] = 0.312;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["m1"] = tmp;

			tmp = args["m2"];
			args["m2"] = -23.999;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["m2"] = tmp;

			tmp = args["m3"];
			args["m3"] = 444;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["m3"] = tmp;

			tmp = args["m4"];
			args["m4"] = -412.2111;
			Assert.ThrowsException<ArgumentException>(() => formula.CalculateFormula(args));
			args["m4"] = tmp;
		}


		private static double TestDelegate(IReadOnlyDictionary<string, double> args)
		{
			return args["x"] + args["y"] * args["k"];
		}
	}
}
